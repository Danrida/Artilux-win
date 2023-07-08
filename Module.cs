using Autofac;

using ArtiluxEOL.Framework;
using ArtiluxEOL;

//using static Ion.Sdk.DemoApp.Components.WorkspaceFileLoader;
//using static Ion.Sdk.DemoApp.Components.WorkspaceFileExporter;
//using static Ion.Sdk.DemoApp.Components.AutoSequenceGroupFileLoader;

namespace ArtiluxEOL
{
    internal class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /*builder.Register(context => System.Windows.Application.Current.Dispatcher).AsSelf();
            builder.RegisterType<Views.MainWindow>().SingleInstance();

            builder.RegisterType<Padfx>().As<WorkspaceFileLoader>().SingleInstance();
            builder.RegisterType<Atmpx>().As<AutoSequenceGroupFileLoader>().SingleInstance();

            builder.RegisterType<Json>().As<WorkspaceFileExporter>().SingleInstance();
            builder.RegisterType<Xml>().As<WorkspaceFileExporter>().SingleInstance();
            builder.RegisterType<XmlSchemas>().As<WorkspaceFileExporter>().SingleInstance();

            builder.RegisterType<JsonFlat>().As<WorkspaceFileExporter>().SingleInstance();
            builder.RegisterType<XmlFlat>().As<WorkspaceFileExporter>().SingleInstance();
            builder.RegisterType<XmlSchemasFlat>().As<WorkspaceFileExporter>().SingleInstance();

            builder.RegisterType<MessagingService>().As<IMessagingService>();
            builder.RegisterType<ExceptionHandler>().As<IExceptionHandler>();


            // register views
            builder.RegisterInstance(PadfxBrowser.ViewInfo).As<IAppViewInfo>().SingleInstance();
            builder.RegisterType<PadfxBrowser>().Named<IAppView>(PadfxBrowser.ViewInfo.Id)
                                                .SingleInstance();

            builder.RegisterInstance(AutoSequences.ViewInfo).As<IAppViewInfo>().SingleInstance();
            builder.RegisterType<AutoSequences>().Named<IAppView>(AutoSequences.ViewInfo.Id)
                                                 .SingleInstance();*/
        }
    }
}
