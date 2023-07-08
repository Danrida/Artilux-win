using MonitorsTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorsTest.Services
{
  public class Helper
    {
        public static List<TestList> GettestLists()
        {
            List < TestList > tl=  new List<TestList> { new TestList { Id=1, Name="HV Atsparumo testas" }, 
                new TestList { Id = 2, Name = "RF-ID testas" },
                new TestList { Id=3, Name="Srovės ir EV komunikacijos testas"},
                new TestList { Id=4, Name="WIFI testas" },
                new TestList { Id=5, Name="GSM testas" },
                new TestList { Id=6, Name="Testas ABC" },
                new TestList { Id=7, Name="Testas DEF" },
                new TestList { Id=8, Name="Testas GHI" }
            };
            foreach (var t in tl)
            {
                t.TestLong = GetTestLong();
                        t.TestResult = GetTestResult();
            }


            return tl;
        }

        public static int GetTestLong()
        {
            Random rnd = new Random();
           int r = rnd.Next(1, 4);
            return r;
        }

        public static bool GetTestResult()
        {
            Random rnd = new Random();
            bool ret = true;
            int r = rnd.Next(1, 20);
            if ( r== 10)
            {
                ret = false;
            }
            return ret;


        }

    }
}
