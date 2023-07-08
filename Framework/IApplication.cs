using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace ArtiluxEOL.Framework
{
    public interface IApplication
    {
        ILifetimeScope BeginLifetimeScope();
        void Log(Exception ex);
    }
}
