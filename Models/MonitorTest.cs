using System;
using System.Collections.Generic;
using System.Linq;
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
}
