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
