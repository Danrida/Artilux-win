using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MonitorsTest.Models
{
  public class MonitorTest
    {
        public int Id { get; set; }
        public string MonitorIds { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int WorkPlaceNr { get; set; }
        public List<TestList> testList { get; set; }
    }

    public class TestList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TestLong { get; set; }

        public bool TestResult { get; set; }
    }

    public class SocketDevList
    {
        public Socket client { get; set; }
        public string Name { get; set; }
        public string TestMsg { get; set; }
        public string Ip { get; set; }
        public int Port_0 { get; set; }
        public int Port_1 { get; set; }
        public int SocketCount { get; set; }
        public int State { get; set; }
        public bool Enable { get; set; }
        public bool Connected { get; set; }
    }

    
}
