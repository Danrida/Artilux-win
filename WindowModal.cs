using MonitorsTest.Models;
using MonitorsTest.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        

        protected Label[,] label;
        int[] lbl_ptr;//label pointeris

        protected Label[,] lblres;
        protected Label[] lblWorkplace;
        static Label lblWait = new Label { Name = "labelWait" };
        Point lblres_point = new Point(x: 20, y: py);

        MonitorTest mtl = new MonitorTest();


        SocketClient Socket_ = new SocketClient();
        public WindowModal(MonitorTest mt)
        {
            WindowMod = this;
            InitializeComponent();
            mtl = mt;
            this.Text = mtl.MonitorIds;
            lblIds.Text = mt.MonitorIds;
            label = new Label[3, 9];
            lblres = new Label[3, 9];
            lblWorkplace = new Label[3];
            lbl_ptr = new int[5];//label pointeris
            backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);

            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics gra = this.panel2.CreateGraphics();
            Pen grey_pen = new Pen(Color.Gray, 2);

            Point ptn1 = new Point(1, 10);
            Point ptn2 = new Point(1, 430);

            Point hor1_1 = new Point(15, 160);
            Point hor1_2 = new Point(260, 160);

            Point hor2_1 = new Point(15, 250);
            Point hor2_2 = new Point(260, 250);

            e.Graphics.DrawLine(grey_pen, ptn1, ptn2);
            e.Graphics.DrawLine(grey_pen, hor1_1, hor1_2);
            e.Graphics.DrawLine(grey_pen, hor2_1, hor2_2);
        }

        private void TestModal_Load(object sender, EventArgs e)
        {
            int panel_width = this.panel3.Width;
            int screen_width = mtl.Width;
            int panel_position_x = ((screen_width / 2) - (panel_width / 2));

            int panel_height = this.panel3.Height;
            int screen_height = mtl.Height;
            int panel_position_y = ((screen_height / 2) - (panel_height / 2));

            Point panel3_pos = new Point(x: panel_position_x, y: panel_position_y);
            this.Location = Screen.AllScreens[mtl.Id].WorkingArea.Location;
            
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel3.Location = panel3_pos;
            redraw_tests_lables();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {  
            panelTestResult.Controls.Clear();
            redraw_tests_lables();
            lblResult.Text = "Result";
            lblResult.BackColor = Color.LightGray;
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 1;
            lbl_ptr[0] = 1;
            lbl_ptr[1] = 1;
            lbl_ptr[2] = 1;
            lbl_ptr[3] = 1;
            lbl_ptr[4] = 1;
            int py = 10;
            DateTime stime = DateTime.Now;
            mtl.testList = Helper.GettestLists();


            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
      
            try
            {
                    foreach (var test in mtl.testList)
                {
                    ProgressBar progress = new ProgressBar();
                    progress.Name = "progres" + i;
                    progress.Style = ProgressBarStyle.Continuous;
                    lblres_point = new Point(x: 20, y: py+5);
                    progress.Location = lblres_point;
                    
                    lblres[mtl.Id, lbl_ptr[mtl.Id]] = new Label { Name = "labelres" + test.Id.ToString() };
                    lblres[mtl.Id, lbl_ptr[mtl.Id]].Location = lblres_point;
                    lblres[mtl.Id, lbl_ptr[mtl.Id]].Font = SymbolFont;
                    label[mtl.Id, lbl_ptr[mtl.Id]].Font = LargeFont;
                    label[mtl.Id, lbl_ptr[mtl.Id]].ForeColor = Color.Black;

                    Action action = () =>
                    {
                        btnStart.Visible = false;
                        lblWait.Visible = false;
                        panelTestResult.Controls.Add(progress);
                        lblLong.Text = Socket_.UnixTimeNow().ToString();
                    };
                    if (InvokeRequired)
                    {
                        Invoke(action);
                    }
                    else
                    {
                        action();
                    }
                    Thread.Sleep(test.TestLong * 1000);
                    Action actionE = () =>
                    {
                        progress.Visible = false;
                        
                        if (test.TestResult)
                        {
                            //char characters = System.Text.Encoding.ASCII.GetChars(129);
                            //string chara = string.Join("", characters);
                            lblres[mtl.Id, lbl_ptr[mtl.Id]].Text = ""; //OK symbol
                            lblres[mtl.Id, lbl_ptr[mtl.Id]].ForeColor = Color.Green;
                        }
                        else
                        {
                            lblres[mtl.Id, lbl_ptr[mtl.Id]].Text = ""; //FAIL symbol
                            lblres[mtl.Id, lbl_ptr[mtl.Id]].ForeColor = Color.Red;
                            //lblres.Font = LargeFont;
                        }
                        panelTestResult.Controls.Add(lblres[mtl.Id, lbl_ptr[mtl.Id]]);
                    };
                    if (InvokeRequired)
                    {
                        Invoke(actionE);
                    }
                    else
                    {
                        actionE();
                    }
                    Thread.Sleep(test.TestLong * 1000);

                    py = py + 45;

                    lbl_ptr[mtl.Id]++;
                    i++;

                    //DateTime currenttime = DateTime.Now;
                    //label[mtl.Id, lbl_ptr[mtl.Id]].Text = string.Format("Trukmė {0}:{1}", currenttime.Subtract(stime).Minutes, currenttime.Subtract(stime).Seconds);
                    //lblLong.Text = string.Format("Trukmė {0}:{1}", currenttime.Subtract(stime).Minutes, currenttime.Subtract(stime).Seconds);
                }

            }
            catch (Exception err)
            {
                var x = err;
            }
            DateTime etime = DateTime.Now;
            Action actionS = () =>
            {
                lblLong.Text = string.Format("Trukmė {0}:{1}", etime.Subtract(stime).Minutes, etime.Subtract(stime).Seconds);

                if (mtl.testList.Where(it => it.TestResult == false).Count() == 0)
                {
                    lblResult.Text = "PASS";
                    lblResult.BackColor = Color.MediumAquamarine;
                }
                else
                {  
                    lblResult.Text = "FAIL";
                    lblResult.ForeColor = Color.Red;
                }

            btnStart.Visible = true;
            };
            if (InvokeRequired)
            {
                Invoke(actionS);
            }
            else
            {
                actionS();
            }
        }

        void redraw_tests_lables()
        {
            int py = 10;
            int i = 1;

            lbl_ptr[0] = 1;
            lbl_ptr[1] = 1;
            lbl_ptr[2] = 1;
            lbl_ptr[3] = 1;
            lbl_ptr[4] = 1;

            mtl.testList = Helper.GettestLists();

            foreach (var test in mtl.testList)
            {
                //System.Diagnostics.Debug.Print($"START_BTN: = {0}");
                label[mtl.Id, lbl_ptr[mtl.Id]] = new Label { Name = "label" + test.Id.ToString(), Text = test.Name, AutoSize = true, Location = new Point(x: 60, y: py) };
                label[mtl.Id, lbl_ptr[mtl.Id]].Font = LargeFont;
                lblWorkplace[mtl.Id] = new Label { Name = "lblWorkplace" + test.Id.ToString(), Text = mtl.Id.ToString(), AutoSize = true, Location = new Point(x: 35, y: 0), Font = new Font("Microsoft Sans Serif", 48) };
                label[mtl.Id, lbl_ptr[mtl.Id]].ForeColor = Color.Gray;
                lblWait = new Label { Name = "labelWait" + test.Id.ToString() };
                lblres_point = new Point(x: 20, y: py + 5);
                lblWait.Location = lblres_point;
                lblWait.Font = SymbolFont;

                Action action = () =>
                {
                    lblWait.Text = ""; //WAIT symbol
                    panelTestResult.Controls.Add(label[mtl.Id, lbl_ptr[mtl.Id]]);
                    panel_wplace.Controls.Add(lblWorkplace[mtl.Id]);
                    //panelTestResult.Controls.Add(lblWait);
                };
                if (InvokeRequired)
                {
                    Invoke(action);
                }
                else
                {
                    action();
                }

                py = py + 45;

                lbl_ptr[mtl.Id]++;

                i++;
            }
        }


        


        private void TestModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Text = lblResult.Text + "-" + this.Text;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.tes.Show();  // re-show original form

            
        }

        
    }
}
