using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ion.Sdk.Ddr;
using Ion.Sdk.Ddr.Extensions;
//using Ion.Sdk.DemoApp.Views.ViewModels;
using Ion.Sdk.Ici;
using Ion.Sdk.Idi;
using System.Windows.Forms;
using static Ion.Sdk.Ici.Channel.BlackBox.Message;
using System.Threading;
using ArtiluxEOL.Framework;

namespace ArtiluxEOL
{
    public partial class Metrel_bb : Form
    {
        //private readonly IApplication application;
        //private readonly ViewModel viewModel;
        public static Metrel_bb MetrelBlackBox;

        private Request currentRequest = null;
        private readonly InstrumentChannelTaskSource serChannel = null;
        private readonly IBlackBoxChannelTaskSource channel;


        

        public List<IDisposable> subscriptions = new List<IDisposable>();
        public Metrel_bb()
        {


            serChannel = Channel.Serial.Open("COM10");
            channel = Channel.BlackBox.Create(serChannel, "");

            subscriptions.Add(Channel.BlackBox.Get<Notification>()
                                                       .Subscribe(OnBlackBoxRequest));

                subscriptions.Add(Channel.BlackBox.Get<Question>()
                                                       .Subscribe(OnBlackBoxRequest));
                subscriptions.Add(Channel.BlackBox.Get<Channel.BlackBox.Message.Keyboard>()
                                                       .Subscribe(OnBlackBoxRequest));

            subscriptions.Add(Channel.BlackBox.Get<IntermediateResult>()
                                                                   .Subscribe(message =>
                                                                   {
                                                                   //InfoTexts.Add($"Intermediate result [{message.Result.Info.GetText(Configuration.DefaultLanguage)}] = {message.Result.Value?.GetText() ?? "null"}; Status = {message.Result.Status}");
                                                                   System.Diagnostics.Debug.Print($"Intermediate result [{message.Result.Info.GetText(Configuration.DefaultLanguage)}] = {message.Result.Value?.GetText() ?? "null"}; Status = {message.Result.Status}");
                                                                   }));

            subscriptions.Add(Channel.BlackBox.Get<AutoSequencePause>()
                                                       .Subscribe(message =>
                                                       {
                                                           //InfoTexts.Add("AutoSequence Paused...");
                                                           System.Diagnostics.Debug.Print($"AutoSequence Paused...");
                                                           RefreshActionsAndKeys();
                                                       }));

            subscriptions.Add(Channel.BlackBox.Get<AutoSequenceStepEndDecision>()
                                                   .Subscribe(message =>
                                                   {
                                                       //InfoTexts.Add("AutoSequence step ended. Awaiting decision...");
                                                       System.Diagnostics.Debug.Print($"AutoSequence step ended. Awaiting decision...");
                                                       RefreshActionsAndKeys();
                                                   }));

            subscriptions.Add(Channel.BlackBox.Get<AutoSequenceResult>()

                .Subscribe(message =>
                {
                    //InfoTexts.Add($"Measured: {message.Test.Measurement.Info.GetText(Configuration.DefaultLanguage)} =  {message.Test.Measurement.Results.FirstOrDefault()?.Value?.GetText() ?? "null"}; Status = {message.Test.Measurement.Status}");
                    System.Diagnostics.Debug.Print($"Measured: {message.Test.Measurement.Info.GetText(Configuration.DefaultLanguage)} =  {message.Test.Measurement.Results.FirstOrDefault()?.Value?.GetText() ?? "null"}; Status = {message.Test.Measurement.Status}");  
                }));


            InitializeComponent();
        }




        public void ExecuteAction(string action)
        {
            System.Diagnostics.Debug.Print($"action: {action}");
            if (string.IsNullOrEmpty(action)) return;
            channel.ExecuteAction(action);

            RefreshActionsAndKeys();

        }

        public void SendResponse(object parameter)
        {
            System.Diagnostics.Debug.Print($"resp: {parameter}");
            var request = currentRequest;
            if (request == null) return;
            var response = request.CreateResponse(parameter);
            Channel.BlackBox.Publish(response);
            currentRequest = null;

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

        public void RequestActions()
        { 
             channel.RequestAvailableActions();
        }
       

        public void RequestKeys()
        {
            channel.RequestAvailableKeys();
        }
                
        private void OnBlackBoxRequest(Request request)
        { 
            currentRequest = request;
        }
        



        private void OnBlackBoxError(Error errorMessage)
        {
            Trace.TraceError($"BlackBox Error:\n{errorMessage.ErrorNumber}; {errorMessage.Description}");
            //InfoTexts.Add($"ERROR: {errorMessage.ErrorNumber}; {errorMessage.Description}");
        }

        public Metrel_bb(IContainer container)
        {
            container.Add(this);

            MetrelBlackBox = this;
            InitializeComponent();
            
        }
        //private Lazy<IBlackBoxChannelTaskSource> lazyChannel;
        //private IBlackBoxChannelTaskSource Channel => lazyChannel.Value;


        public async Task PolarityNoInteraction()
        {
            

            var region = new Region(1000, 1);
            var measurementInfo = DataDisplayInfos.MeasurementInfoList.Get(4);    //Polarity
            var measurement = new EmptyMeasurement(measurementInfo.Id, region);
            var test = new SingleTest(measurement);
            //var result = channel.Start(test).Result;

            var bb_channel = await Task.Run(() => channel);

            //InfoTexts.Add("Running...");
            System.Diagnostics.Debug.Print($"Running...");

            Action timeoutAction = null;
            //if (!showIntermediateResults)
            /*timeoutAction = () =>
            {
                if (CurrentRequest == null)
                    channel.Stop();
            };*/
            ThreadPool.QueueUserWorkItem(state =>
            {
                // request actions/keys after test started with some delay.
                Thread.Sleep(1500);
                channel.RequestAvailableActions();
                channel.RequestAvailableKeys();
            });

            var result = channel.Start(test, sendIntermediateResuls: true, touchTestEnabled: false).Result;

            
            Console.WriteLine($"Status: {result.Status}");

        }

        public async Task StartAutoSequence(object parameter)
        {
            string AutoSequenceName = "EVSE 3p Vent trip";

            bool showIntermediateResults = true;

            var region = new Region(1000, 1);
            if (parameter is string port)
            {
                try
                {
                    //InfoTexts.Clear();
                    //InfoTexts.Add("Starting...");
                    System.Diagnostics.Debug.Print($"Starting...");

                    // resolve channel in background.
                    var bb_channel = await Task.Run(() => channel);

                    //InfoTexts.Add("Running...");
                    System.Diagnostics.Debug.Print($"Running...");

                    ThreadPool.QueueUserWorkItem(state =>
                    {
                        // request actions/keys after test started with some delay.
                        Thread.Sleep(1500);
                        channel.RequestAvailableActions();
                        channel.RequestAvailableKeys();
                    });

                    var status = await channel.StartAutoSequence(AutoSequenceName, region, false, showIntermediateResults);
                    //InfoTexts.Add($"Finished (status = {status}).");
                    System.Diagnostics.Debug.Print($"Finished (status = {status}).");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print($"Autosec start failed!!! ");
                }
            }
        }

        public async Task Stop()
        {
            
                
                await Task.Run(() => channel?.Stop());
            
        }


    }


}
