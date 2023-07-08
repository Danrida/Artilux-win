using Autofac;
using System.ComponentModel;
using Ion.Sdk.Ddr;
using Ion.Sdk.Ddr.Extensions;
//using Ion.Sdk.DemoApp.Views.ViewModels;
using ici =  Ion.Sdk.Ici;
using Ion.Sdk.Idi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Input;
using ArtiluxEOL.Framework;
using static Ion.Sdk.Ici.Channel.BlackBox.Message;
using RegionInfo = Ion.Sdk.Ddr.RegionInfo;
using Ion.Sdk.Ici;
using System.Data.Linq;
using System.Windows.Forms;

namespace ArtiluxEOL
{
    public partial class Metrel : Form
    {
        

        public readonly IApplication application;
        public readonly ViewModel viewModel;
        public readonly ILifetimeScope scope;

        public ViewModel DataContext { get; }

        public IAppViewInfo Info => ViewInfo;

        bool IsOnline = true;


        public string SelectedPort
        {
            //get => (string)GetValue(SelectedPortProperty);
            //internal => StartSingleTestCommand("dsf");
            set => viewModel.StartSingleTest("");
        }

        public int[] AvailableBaudRates => new[] { 9600, 57600, 115200 };

        public Framework.Command RefreshAvailablePortsCommand { get; }
        public Framework.Command StartSingleTestCommand { get; }
        public Framework.Command StartAutoSequenceCommand { get; }
        public Framework.Command StopCommand { get; }
        public Framework.Command RequestKeysCommand { get; }
        public Framework.Command SendKeyCommand { get; }
        public Framework.Command RequestActionsCommand { get; }
        public Framework.Command ExecuteActionCommand { get; }
        public Framework.Command SendResponseCommand { get; }
        public Framework.Command SelectRegionCommand { get; }
        public Framework.Command SelectMeasurementCommand { get; }


        public static Metrel MetrelBB;

        public Metrel()
        {
            this.application = application;

            
            //RefreshAvailablePortsCommand = Framework.Command.Create(parameter => AvailablePorts = System.IO.Ports.SerialPort.GetPortNames());
            StartSingleTestCommand = Framework.Command.CreateAsync(parameter => viewModel.StartSingleTest("COM6"), parameter => IsOnline == true && viewModel.SelectedRegion != null);
            StartAutoSequenceCommand = Framework.Command.CreateAsync(parameter => viewModel.StartAutoSequence("COM6"), parameter => IsOnline == true && !string.IsNullOrEmpty(viewModel.AutoSequenceName) && viewModel.SelectedRegion != null);
            StopCommand = Framework.Command.CreateAsync(parameter => viewModel.Stop(), parameter => IsOnline == true && viewModel?.BlackBoxProgress.IsBusy == true);
            RequestKeysCommand = Framework.Command.Create(parameter => viewModel.RequestKeys(), parameter => IsOnline == true && viewModel?.BlackBoxProgress.IsBusy == true);
            SendKeyCommand = Framework.Command.Create(parameter => viewModel.SendKey(parameter as string), parameter => IsOnline == true && viewModel?.BlackBoxProgress.IsBusy == true);
            RequestActionsCommand = Framework.Command.Create(parameter => viewModel.RequestActions(), parameter => IsOnline == true && viewModel?.BlackBoxProgress.IsBusy == true);
            ExecuteActionCommand = Framework.Command.Create(parameter => viewModel.ExecuteAction(parameter as string), parameter => IsOnline == true && viewModel?.BlackBoxProgress.IsBusy == true);
            SendResponseCommand = Framework.Command.Create(parameter => viewModel.SendResponse(parameter));
            SelectRegionCommand = Framework.Command.Create(parameter => viewModel.SelectedRegion = parameter as RegionInfo, parameter => IsOnline == true && viewModel?.BlackBoxProgress.IsBusy == false);
            SelectMeasurementCommand = Framework.Command.Create(parameter => viewModel.SelectedMeasurementInfo = parameter as MeasurementInfo, parameter => IsOnline == true && viewModel?.BlackBoxProgress.IsBusy == false);
            
            InitializeComponent();
            //scope = application.BeginLifetimeScope();
            //DataContext = viewModel = scope.Resolve<ViewModel>();

            MetrelBB = this;
        }

        public Metrel(Autofac.IContainer container)
        {
            //container.Add(this);

            InitializeComponent();
        }

        

        public void EvaluateCommands()
        {
            RefreshAvailablePortsCommand.RaiseCanExecuteChanged();
            StartSingleTestCommand.RaiseCanExecuteChanged();
            StartAutoSequenceCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
            RequestKeysCommand.RaiseCanExecuteChanged();
            SendKeyCommand.RaiseCanExecuteChanged();
            RequestActionsCommand.RaiseCanExecuteChanged();
            ExecuteActionCommand.RaiseCanExecuteChanged();
            SelectRegionCommand.RaiseCanExecuteChanged();
            SelectMeasurementCommand.RaiseCanExecuteChanged();
            
        }


        

        public static readonly IAppViewInfo ViewInfo = new AppViewInfo { Id = nameof(Metrel), Name = "Metrel" };

        public sealed class ViewModel : BindableObject, IDisposable
        {
            private readonly IApplication application;

            #region -=Properties/Fields=-

            private const int polarityMeasurementId = 147;

            //private string selectedPort = string.Empty;
            private string selectedPort = "COM6";

            private Lazy<ici.IBlackBoxChannelTaskSource> lazyChannel;
            private readonly List<IDisposable> subscriptions = new List<IDisposable>();

            private const string StatusIdle = "Status - Idle";

            private Request currentRequest = null;

            public Request CurrentRequest
            {
                get => currentRequest;
                private set => Set(ref currentRequest, value);
            }

            private string actionInfo = StatusIdle;

            public string ActionInfo
            {
                get => actionInfo;
                set => Set(ref actionInfo, value);
            }

            private RegionInfo selectedRegion = null;

            public RegionInfo SelectedRegion
            {
                get => selectedRegion;
                set
                {
                    Set(ref selectedRegion, value);
                    LoadMeasurements(selectedRegion, instrument);
                }
            }

            public ObservableCollection<RegionInfo> RegionsList { get; } = new ObservableCollection<RegionInfo>();


            private MeasurementInfo selectedMeasurementInfo = null;

            public MeasurementInfo SelectedMeasurementInfo
            {
                get => selectedMeasurementInfo;
                set => Set(ref selectedMeasurementInfo, value);
            }


            private List<MeasurementInfo> measurementsList = new List<MeasurementInfo>();
            public List<MeasurementInfo> MeasurementsList
            {
                get => measurementsList;
                set => Set(ref measurementsList, value);
            }

            private bool touchPreTest = true;
            public bool TouchPreTest
            {
                get => touchPreTest;
                set => Set(ref touchPreTest, value);
            }

            private string blackBoxPassword;
            public string BlackBoxPassword
            {
                get => blackBoxPassword;
                set => Set(ref blackBoxPassword, value);
            }

            private bool showIntermediateResults = true;
            public bool ShowIntermediateResults
            {
                get => showIntermediateResults;
                set => Set(ref showIntermediateResults, value);
            }

            private string autoSequenceName = "";
            public string AutoSequenceName
            {
                get => autoSequenceName;
                set => Set(ref autoSequenceName, value);
            }
          
            public ObservableCollection<string> InfoTexts { get; } = new ObservableCollection<string>();

            private List<string> actions = new List<string>();

            public List<string> Actions
            {
                get => actions;
                set => Set(ref actions, value);
            }

            private List<string> keys = new List<string>();

            public List<string> Keys
            {
                get => keys;
                set => Set(ref keys, value);
            }

            public ici.ProgressIndicator Progress { get; } = new ici.ProgressIndicator();
            public ici.ProgressIndicator BlackBoxProgress { get; } = new ici.ProgressIndicator();
            private ici.IBlackBoxChannelTaskSource Channel => lazyChannel.Value;

            private ici.IInstrumentInfo instrument = null;
            public ici.IInstrumentInfo Instrument
            {
                get => instrument;
                set
                {
                    if (Set(ref instrument, value))
                    {
                        LoadRegions(instrument);
                        LoadMeasurements(SelectedRegion, instrument);
                    }
                }
            }

            private string Port
            {
                get => selectedPort;
                set
                {
                    var newValue = value ?? string.Empty;
                    if (Set(ref selectedPort, newValue, () => string.CompareOrdinal(selectedPort, newValue) != 0))
                        ResetChannel();
                }
            }

            #endregion

            public ViewModel(IApplication application)
            {
                this.application = application;
                ResetChannel();

                System.Diagnostics.Debug.Print($"Subscription");

                #region -=Subscribe to BlackBox messages=-


                subscriptions.Add(ici.Channel.BlackBox.Get<Error>()
                                                       .ObserveOnDispatcher()
                                                       .Subscribe(OnBlackBoxError));

                subscriptions.Add(ici.Channel.BlackBox.Get<Notification>()
                                                       .ObserveOnDispatcher()
                                                       .Subscribe(OnBlackBoxRequest));

                subscriptions.Add(ici.Channel.BlackBox.Get<Question>()
                                                       .ObserveOnDispatcher()
                                                       .Subscribe(OnBlackBoxRequest));
                subscriptions.Add(ici.Channel.BlackBox.Get<ici.Channel.BlackBox.Message.Keyboard>()
                                                       .ObserveOnDispatcher()
                                                       .Subscribe(OnBlackBoxRequest));

                subscriptions.Add(ici.Channel.BlackBox.Get<AvailableActions>()
                                                       .ObserveOnDispatcher()
                                                       .Subscribe(message => Actions = message.Actions));

                subscriptions.Add(ici.Channel.BlackBox.Get<AvailableKeys>()
                                                       .ObserveOnDispatcher()
                                                       .Subscribe(message => Keys = message.Keys));

                subscriptions.Add(ici.Channel.BlackBox.Get<IntermediateResult>()
                                                       .ObserveOnDispatcher()
                                                       .Subscribe(message =>
                                                       {
                                                           InfoTexts.Add($"Intermediate result [{message.Result.Info.GetText(Configuration.DefaultLanguage)}] = {message.Result.Value?.GetText() ?? "null"}; Status = {message.Result.Status}");
                                                           System.Diagnostics.Debug.Print($"Intermediate result [{message.Result.Info.GetText(Configuration.DefaultLanguage)}] = {message.Result.Value?.GetText() ?? "null"}; Status = {message.Result.Status}");
                                                       }));

                subscriptions.Add(ici.Channel.BlackBox.Get<TouchTest>()
                             .ObserveOnDispatcher()
                             .Subscribe(message =>
                             {
                                 InfoTexts.Add($"Touch test: {message.Option}");
                                 RefreshActionsAndKeys();
                             }));

                subscriptions.Add(ici.Channel.BlackBox.Get<AutoSequencePause>()
                                                       .ObserveOnDispatcher()
                                                       .Subscribe(message =>
                                                       {
                                                           InfoTexts.Add("AutoSequence Paused...");
                                                           RefreshActionsAndKeys();
                                                       }));

                subscriptions.Add(ici.Channel.BlackBox.Get<AutoSequenceStepEndDecision>()
                                                       .ObserveOnDispatcher()
                                                       .Subscribe(message =>
                                                       {
                                                           InfoTexts.Add("AutoSequence step ended. Awaiting decision...");
                                                           RefreshActionsAndKeys();
                                                       }));

                subscriptions.Add(ici.Channel.BlackBox.Get<AutoSequenceResult>()
                    .ObserveOnDispatcher()
                    .Subscribe(message =>
                    {
                        InfoTexts.Add($"Measured: {message.Test.Measurement.Info.GetText(Configuration.DefaultLanguage)} =  {message.Test.Measurement.Results.FirstOrDefault()?.Value?.GetText() ?? "null"}; Status = {message.Test.Measurement.Status}");
                    }));

                #endregion
            }

            public async Task StartSingleTest(object parameter)
            {
                if (selectedRegion == null)
                {
                    InfoTexts.Add("Region not selected.");
                    return;
                }
                var region = new Region((uint)(selectedRegion.MasterRegionInfoId ?? 0), (uint)selectedRegion.Id);
                var measurementInfo = SelectedMeasurementInfo;
                if (parameter is string port && measurementInfo != null)
                {
                    try
                    {
                        InfoTexts.Clear();
                        InfoTexts.Add("Starting...");
                        System.Diagnostics.Debug.Print($"Starting... ");

                        BlackBoxProgress.IsBusy = true;
                        ActionInfo = "Starting single test...";
                        System.Diagnostics.Debug.Print($"Starting single test...");

                        Port = port;
                        
                        var measurement = new EmptyMeasurement(MeasurementsList[0].Id, region);
                        //var measurement = new EmptyMeasurement(measurementInfo.Id, region);
                        var test = new SingleTest(measurement);

                        // resolve channel in background.
                        var channel = await Task.Run(() => Channel);

                        ActionInfo = "Single test - running...";

                        InfoTexts.Add("Running...");
                        System.Diagnostics.Debug.Print($"Running...");

                        Action timeoutAction = null;
                        if (!showIntermediateResults)
                            timeoutAction = () =>
                            {
                                if (CurrentRequest == null)
                                    channel.Stop();

                                
                            };
                        ThreadPool.QueueUserWorkItem(state =>
                        {
                            // request actions/keys after test started with some delay.
                            Thread.Sleep(1500);
                            channel.RequestAvailableActions();
                            channel.RequestAvailableKeys();
                        });

                        var result = await channel.Start(test, timeoutAction, sendIntermediateResuls: showIntermediateResults, touchTestEnabled: touchPreTest);

                        InfoTexts.Add($"Returned: {result.Measurement.Parameters.Count()} parameters, {result.Measurement.Results.Count()} limits, {result.Measurement.Results.Count()} results.");
                        InfoTexts.Add($"Finished (status = {result.Measurement.Status}).");
                        System.Diagnostics.Debug.Print($"Finished (status = {result.Measurement.Status}).");
                    }
                    catch (Exception ex)
                    {
                        application.Log(ex);
                        InfoTexts.Add(ex.Message);
                    }
                    finally
                    {
                        Reset();
                    }
                }
            }

            public async Task StartAutoSequence(object parameter)
            {
                if (selectedRegion == null)
                {
                    InfoTexts.Add("Region not selected.");
                    return;
                }

                var region = new Region((uint)(selectedRegion.MasterRegionInfoId ?? 0), (uint)selectedRegion.Id);
                if (parameter is string port)
                {
                    try
                    {
                        InfoTexts.Clear();
                        InfoTexts.Add("Starting...");

                        BlackBoxProgress.IsBusy = true;
                        ActionInfo = "Starting AutoSequence...";

                        Port = port;

                        // resolve channel in background.
                        var channel = await Task.Run(() => Channel);

                        ActionInfo = "AutoSequence - running...";
                        InfoTexts.Add("Running...");

                        ThreadPool.QueueUserWorkItem(state =>
                        {
                            // request actions/keys after test started with some delay.
                            Thread.Sleep(1500);
                            channel.RequestAvailableActions();
                            channel.RequestAvailableKeys();
                        });

                        var status = await channel.StartAutoSequence(AutoSequenceName, region, false, showIntermediateResults);
                        InfoTexts.Add($"Finished (status = {status}).");
                    }
                    catch (Exception ex)
                    {
                        application.Log(ex);
                        InfoTexts.Add(ex.Message);
                    }
                    finally
                    {
                        Reset();
                    }
                }
            }

            public async Task Stop()
            {
                if (lazyChannel.IsValueCreated)
                {
                    await Task.Run(() => Channel?.Stop());
                }
            }

            public void RequestKeys()
                => Channel.RequestAvailableKeys();

            public void SendKey(string key)
            {
                if (string.IsNullOrEmpty(key)) return;
                Channel.SendKey(key);

                RequestActions();
                RequestKeys();
            }

            public void RequestActions()
                => Channel.RequestAvailableActions();

            public void ExecuteAction(string action)
            {
                if (string.IsNullOrEmpty(action)) return;
                Channel.ExecuteAction(action);

                RefreshActionsAndKeys();

            }

            public void SendResponse(object parameter)
            {
                var request = CurrentRequest;
                if (request == null) return;
                var response = request.CreateResponse(parameter);
                ici.Channel.BlackBox.Publish(response);
                CurrentRequest = null;

                RefreshActionsAndKeys();
            }

            private void RefreshActionsAndKeys()
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Thread.Sleep(1500);
                    RequestActions();
                    RequestKeys();
                });
            }

            private void ResetChannel()
                => lazyChannel = new Lazy<ici.IBlackBoxChannelTaskSource>(() => ici.Channel.BlackBox.Create(
                    ici.Channel.Serial.Open(selectedPort), blackBoxPassword: blackBoxPassword));

            public void Reset()
            {
                CurrentRequest = null;
                BlackBoxProgress.IsBusy = false;
                ActionInfo = StatusIdle;
            }

            public int startSingle()
            {
                StartSingleTest("COM6");

                return 0;
            }

            private List<InstrumentVariantInfo> GetInstrumentsByProfile(ici.IInstrumentInfo instrument)
            {
                if (instrument == null)
                    return new List<InstrumentVariantInfo>();

                var profile = instrument.GetAllProperties().First(_ => _.Key == "ProfileCode").Value;
                return new List<InstrumentVariantInfo>(from item in DataDisplayInfos.InstrumentVariantInfoList
                                                       where item.Code == profile
                                                       select item);
            }

            private void LoadRegions(ici.IInstrumentInfo instrument)
            {
                RegionsList.Clear();
                List<InstrumentVariantInfo> instrumentsByProfile = GetInstrumentsByProfile(instrument);
                if (instrumentsByProfile.Any())
                {
                    var instrumentInfo = instrumentsByProfile.First();
                    foreach (var region in DataDisplayInfos.RegionInfoList.Where(
                        item => instrumentInfo.SimpleRegionInfoList.Exists(_ => _.Id == item.MasterRegionInfoId && _.GetChildRegions().Any(id => id == item.Id))))
                    {
                        RegionsList.Add(region);
                    }
                }

                SelectedRegion = RegionsList.FirstOrDefault();
            }

            private void LoadMeasurements(RegionInfo region, ici.IInstrumentInfo instrument)
            {
                if (region == null) return;
                List<InstrumentVariantInfo> instrumentsByProfile = GetInstrumentsByProfile(instrument);

                this.MeasurementsList = new List<MeasurementInfo>(from item in DataDisplayInfos.MeasurementInfoList
                                                                  where item.SimpleRegionInfoList.Exists(_ => _.Id == region.MasterRegionInfoId && _.GetChildRegions().Any(id => id == region.Id))
                                                                        && (instrumentsByProfile == null || item.InstrumentSupportInfoList.Any(i => instrumentsByProfile.Any(info => info.Id == i.InstrumentVariant.Id)))
                                                                  orderby item.GetText(Configuration.DefaultLanguage)
                                                                  select item);

                if (selectedMeasurementInfo == null)
                    this.SelectedMeasurementInfo = this.MeasurementsList.FirstOrDefault(_ => _.Id == polarityMeasurementId); // select Polarity by default.
                else
                    this.SelectedMeasurementInfo = this.MeasurementsList.FirstOrDefault(_ => _.Id == selectedMeasurementInfo.Id);
            }

            #region -=IDisposable members=-

            private bool isDisposed = false;
            private readonly object disposeSync = new object();
            public void Dispose()
            {
                lock (disposeSync)
                {
                    if (!isDisposed)
                    {

                        foreach (var subscription in subscriptions)
                            subscription.Dispose();
                        subscriptions.Clear();
                        isDisposed = true;
                    }
                }
            }
            #endregion

            #region -=BlackBox message handlers=-

            private void OnBlackBoxRequest(Request request)
                => CurrentRequest = request;

            private void OnBlackBoxError(Error errorMessage)
            {
                Trace.TraceError($"BlackBox Error:\n{errorMessage.ErrorNumber}; {errorMessage.Description}");
                InfoTexts.Add($"ERROR: {errorMessage.ErrorNumber}; {errorMessage.Description}");
            }

            #endregion


        }

      
    }
}
