using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace ArtiluxEOL.Framework
{
    public class MessagingService : IMessagingService
    {
        private readonly ILifetimeScope scope;

        public MessagingService(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public void Show(Message message)
        {
            //var shell = scope.Resolve<ArtiluxEOL.MainWindow>() as IMessagingService;
            //shell?.Show(message);
            System.Diagnostics.Debug.Print($"IMessagingService!!!:");
        }
    }
}
