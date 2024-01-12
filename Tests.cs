using MonitorsTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace ArtiluxEOL
{
    public partial class Tests : Component
    {
        public Tests()
        {
            InitializeComponent();
        }

        public Tests(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        Test_struc[] test = Main.main.Test;

        SocketClient Socket_ = new SocketClient();

        /*public void hv_pramusimo_testas(int work_place_nr)
        {
            var net_dev = Main.main.network_dev[DevType.GWINSTEK_HV_TESTER];
            long time;

            int test_nr = test[work_place_nr].nr;
            int test_state = test[work_place_nr].test_type[test_nr].state;

            if (test[work_place_nr].all_tests_state == EvseTestState.TEST_STARTING)
            {
                test[work_place_nr].all_tests_state = EvseTestState.TEST_IN_PROGRESS;
            }

            switch (test_state)
            {
                case CurrentTestState.CONNECT_TO_TESTER:
                    break;
                case CurrentTestState.GET_TEST_PARAMS:
                    break;
                case CurrentTestState.SET_TEST_PARAMS:
                    break;
                case CurrentTestState.TESTING:
                    break;
                case CurrentTestState.HANDLE_RESULT:
                    break;
                case CurrentTestState.DONE:
                    break;
            }



            net_dev.TestType = 0;
            net_dev.State = NetDev_State.SELECT_TEST;
           
        }*/
    }
}
