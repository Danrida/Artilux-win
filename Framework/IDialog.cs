using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtiluxEOL.Framework
{
    public interface IDialog
    {
        event EventHandler Closed;
    }

    public interface IConnectedDialog:IDialog
    {
        string Port { get; set; }
        int BaudRate { get; set; }
    }
}
