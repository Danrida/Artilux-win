using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace ArtiluxEOL.Framework
{
    /// <summary>
    /// Exception handler service.
    /// </summary>
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILifetimeScope scope;

        public ExceptionHandler(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public void Handle(Exception ex)
        {
            //var shell = scope.Resolve<Views.MainWindow>() as IExceptionHandler;
            //shell?.Handle(ex);
            System.Diagnostics.Debug.Print($"EXCEPTION!!!:");
        }
    }
}
