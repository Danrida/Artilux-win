using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtiluxEOL.Framework
{
    public class Message
    {
        public string Subject { get; set; }
        public string Text { get; set; }
    }

    public class CriticalMessage : Message { }
}
