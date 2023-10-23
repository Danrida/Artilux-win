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

    public class WorkplaceList
    {
        public UInt64 WorplaceMonitorID { get; set; }
        public bool Enable { get; set; }
        public int BacodePort { get; set; }
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
        public int SubState { get; set; }
        public int StateBefore { get; set; }
        public bool ReceiveRunning { get; set; }
        public bool Enable { get; set; }
        public bool Connected { get; set; }

        public string Cmd { get; set; }
        public string Resp { get; set; }
        public int RespPktCount { get; set; }

        public string TestParam { get; set; }
        public bool NewResp { get; set; }

        public int ReceiveWaitSec { get; set; }

        public long TimeStamp { get; set; }
        public bool NewSendData { get; set; }
        public int SendReceiveState { get; set; }
        public int GetSetParamCount { get; set; }

        public int GetSetParamLeft { get; set; }

        public int TestType { get; set; }

        public string[] device_param { get; set; }
    }

    public struct DevLoad_struc
    {
        public bool state;
        public int load_current;
    }

    public struct DevHV_struc
    {
        public string result;
        public string test_type;
    }
    public struct DevSpectrum_struc
    {
        public string min;
        public string max;
        public string atenuation;
    }

    public struct DevEvse_struc
    {
        public string barcode;
        public UInt32 wifi_rssi;
        public string lte_imei;
        public string lte_imsi;
        public UInt32 lte_rssi;
        public UInt32[] voltage;
        public UInt32[] current;
        public UInt32 power;
        public UInt32 energy;
        public UInt32 frequency;
        public UInt32 temperature;
    }

    public struct DevList{
        public DevLoad_struc DevLoad;
        public DevHV_struc DevHV;
        public DevSpectrum_struc DevSpectrum;
        public DevEvse_struc[] DevEvse;
    }

    public static class Evse_State
    {
        public const int READY = 0;
        public const int BARCODE = 1;
        public const int WIFI_SIGNAL = 2;
        public const int LTE_SIGNAL = 3;
        public const int RELAY_ON = 4;
        public const int RELAY_OFF = 5;
        public const int GET_METER = 6;
        public const int GET_RFID = 7;
        public const int ERR = 15;
    }

    public static class NetDev_State
    {
        public const int READY = 0;
        public const int START_TEST = 1;
        public const int GET_PARAM_ALL = 2;
        public const int SET_PARAM = 3;
        public const int END_TEST = 4;
        public const int GET_PARAM = 5;
        public const int SELECT_TEST = 6;
        public const int ERR = 15;
    }

    public static class NetDev_SendState
    {
        public const int IDLE = 0;
        public const int SEND_BEGIN = 1;
        public const int SEND_WAIT = 2;
        public const int SEND_OK = 3;
        public const int SEND_FAIL = 4;
        public const int RECEIVE_WAIT = 5;
        public const int RECEIVE_OK = 6;
        public const int RECEIVE_FAIL = 7;
    }

    public static class NetDev_Test
    {
        public const int TEST_NONE = 0;
        public const int TEST_SELECT = 1;
        public const int TEST_RET_ON = 2;
        public const int TEST_START = 3;
        public const int GET_RESULT = 4;
        public const int SEND_FAIL = 5;
        public const int RECEIVE_WAIT = 6;
        public const int RECEIVE_OK = 7;
        public const int RECEIVE_FAIL = 8;
        public const int GET_CHART_DATA = 9;
        public const int PROCESS_CHART_DATA = 10;
        public const int EVSE_STATE_CHARGE_ON = 11;
        public const int EVSE_STATE_CHARGE_OFF = 12;
        public const int EVSE_RELAY_ON = 13;
        public const int EVSE_RELAY_OFF = 14;
    }

    

    public static class NetDev_Tab
    {
        public const int MAIN_CONTROLLER = 0;
        public const int HW_TESTER = 1;
        public const int SIGLENT = 2;
        public const int ITECH_LOAD = 3;
        public const int BARCODE_1 = 4;
        public const int BARCODE_2 = 5;
        public const int BARCODE_3 = 6;
        public const int RFID_1 = 7;
        public const int RFID_2 = 8;
        public const int RFID_3 = 9;
        public const int EVSE = 10;
        public const int OSCILOSCOPE = 11;
    }

    public static class Siglent_param
    {
        public const int START_X = 0;
        public const int STOP_X = 1;
        public const int ATTENUATION = 2;
        public const int ITECH_LOAD = 3;

        public const int Y_MIN = 13;
        public const int Y_MAX = 14;
        public const int X_INTERVAL = 15;
        public const int X_RANGE = 16;
        public const int X_UNIT = 17;
        public const int Y_UNIT = 18;
        public const int X_UNIT_RAW = 19;
        public const int Y_UNIT_RAW = 20;
    }

    /* public static class DbgType
     {
         public const int MAIN = 0;
         public const int NETWORK = 1;
         public const int USB = 2;
     }*/

    public class DbgType
    {
        public static bool MAIN { get; set; }
        public static bool NETWORK { get; set; }
        public static bool USB { get; set; }
    }

    public class DevType
    {
        public const int MAIN_CONTROLLER = 0;
        public const int GWINSTEK_HV_TESTER = 1;
        public const int ANALYSER_SIGLENT = 2;
        public const int ITECH_LOAD = 3;
        public const int BARCODE_1 = 4;
        public const int BARCODE_2 = 5;
        public const int BARCODE_3 = 6;
    }

    

}
