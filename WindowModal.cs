using MonitorsTest.Models;
using MonitorsTest.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ArtiluxEOL
{
    public partial class WindowModal : Form
    {

        public static WindowModal WindowMod;

        static int py = 10;
        Font LargeFont = new Font("Arial", 20);
        Font SymbolFont = new Font("Wingdings", 20);

        Tests Tests_ = new Tests();

        protected Label[,] label;
        int[] lbl_ptr;//label pointeris

        protected Label[,] lblres;
        protected Label[] lblWorkplace;
        protected Label[,] label_info;
        static Label lblWait = new Label { Name = "labelWait" };
        Point lbl_result_point = new Point(x: 20, y: py);
        Point label_info_point = new Point(x: 20, y: py);

        MonitorTest mtl = new MonitorTest();
        DevList dev_list = Main.main.devList;
        Test_struc[] test = Main.main.Test;
        SocketClient Socket_ = new SocketClient();


        public void SetupUI()
        {
            int usableWidth = Screen.FromControl(this).WorkingArea.Width;
            int usableHeight = Screen.FromControl(this).WorkingArea.Height;
            Panel mainPanel = this.panel_main;

            mainPanel.Left = (usableWidth / 2) - (mainPanel.Width / 2);
            mainPanel.Top = (usableHeight / 2) - (mainPanel.Height / 2);

            this.label_work_pos.Text = (mtl.Id + 1).ToString();

            progressBar_total.Maximum = progressBar_evse_communication.Maximum + progressBar_load_test.Maximum + progressBar_HV_test.Maximum + progressBar_Wifi_test.Maximum + progressBar_GSM_test.Maximum + progressBar_RFID_test.Maximum + progressBar_RCD_test.Maximum;
        }
        public WindowModal(MonitorTest mt)
        {
            WindowMod = this;
            InitializeComponent();
            mtl = mt;
            this.Text = mtl.MonitorIds;
            lblIds.Text = mt.MonitorIds;
            label = new Label[3, 10];
            lblres = new Label[3, 10];
            lblWorkplace = new Label[3];
            label_info = new Label[3, 10];
            lbl_ptr = new int[5];//label pointeris
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics gra = this.panel2.CreateGraphics();
            Pen grey_pen = new Pen(Color.Gray, 2);

            Point ptn1 = new Point(1, 10);
            Point ptn2 = new Point(1, 430);

            Point hor1_1 = new Point(15, 100);
            Point hor1_2 = new Point(200, 100);

            Point hor2_1 = new Point(15, 350);
            Point hor2_2 = new Point(200, 350);

            e.Graphics.DrawLine(grey_pen, ptn1, ptn2);
            e.Graphics.DrawLine(grey_pen, hor1_1, hor1_2);
            e.Graphics.DrawLine(grey_pen, hor2_1, hor2_2);

            label_info_point = new Point(x: 80, y: 120);

            for (int i = 0; i < 3; i++)
            {
                label_info[i, 0] = new Label { Name = "info:" + mtl.Id.ToString(), Font = new Font("Microsoft Sans Serif", 16), ForeColor = Color.DimGray, AutoSize = true };
                label_info[i, 0].Location = label_info_point;
                label_info[i, 0].Text = "- - - - - -";
            }
            panel2.Controls.Add(label_info[mtl.Id, 0]);
        }

        private void TestModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label_info[mtl.Id, 0] != null)//If modal windows were created
            {
                if (dev_list.DevEvse != null)
                {
                    System.Diagnostics.Debug.Print($"barcode: = {dev_list.DevEvse[1].barcode} mtl.Id:{mtl.Id}");
                    label_info[mtl.Id, 0].Text = dev_list.DevEvse[mtl.Id].barcode;
                    //label_info[0, 0].Text = "bar_0";
                    //label_info[1, 0].Text = "bar_1";
                    //label_info[2, 0].Text = "bar_2";
                }
                else
                {
                    //System.Diagnostics.Debug.Print($"DevEvse: = NULL");
                }
            }
        }

        public void Update_Test_Label(String labelText, Color textColor)
        {
            label_Test_State.Text = labelText;
            label_Test_State.ForeColor = textColor;
        }

        public void Update_Timer_Label(long startTime)
        {
            long msElapsed = DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime;
            TimeSpan t = TimeSpan.FromMilliseconds(msElapsed);
            string timerText = "";

            if (t.Minutes < 10)
            {
                timerText += "0";
            }

            timerText += t.Minutes;
            timerText += ":";

            if (t.Seconds < 10)
            {
                timerText += "0";
            }

            timerText += t.Seconds;

            label_Time_elapsed.Text = timerText;
        }

        public void Zero_Timer_Label()
        {
            label_Time_elapsed.Text = "00:00";
        }
    }
}
