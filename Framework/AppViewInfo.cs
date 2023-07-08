using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtiluxEOL.Framework
{
    public interface IAppViewInfo
    {
        string Id { get; }
        string Name { get; }
    }

    public interface IAppView
    {
        IAppViewInfo Info { get; }
    }

    public sealed class AppViewInfo : IAppViewInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
