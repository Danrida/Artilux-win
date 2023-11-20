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
            List < TestList > tl=  new List<TestList> { 
                new TestList { Id=1, Name = "HV Atsparumo Testas" },
                new TestList { Id=2, Name = "HV Pramušimo Testas" },
                new TestList { Id=3, Name = "EVSE Apkrovos Testas" },
                new TestList { Id=4, Name = "EVSE Komunikacijos Testas"},
                new TestList { Id=5, Name = "EVSE Kabelio Užrakto Testas"},
                new TestList { Id=6, Name = "RCD Testas" },
                new TestList { Id=7, Name = "RF-ID Testas" },
                new TestList { Id=8, Name = "WIFI Testas" },
                new TestList { Id=9, Name = "GSM Testas" },
                  
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
