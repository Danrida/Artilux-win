using MonitorsTest.Models;
using MonitorsTest.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);


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
            //System.Diagnostics.Debug.Print($"monitor.id: = {mtl.Id}");

            for (int i = 0; i < 3; i++)
            {
                label_info[i, 0] = new Label { Name = "info:" + mtl.Id.ToString(), Font = new Font("Microsoft Sans Serif", 16), ForeColor = Color.DimGray, AutoSize = true };
                label_info[i, 0].Location = label_info_point;
                label_info[i, 0].Text = "- - - - - -";
            }
            panel2.Controls.Add(label_info[mtl.Id, 0]);
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

            bool stop_test = false;

            // Get the BackgroundWorker that raised this event.
            //BackgroundWorker worker = sender as BackgroundWorker;

            int test_nr = 0;

            int y_pos = 0;

            while (!stop_test)
            {

                switch (test[mtl.Id].all_tests_state)
                {
                    case EvseTestState.NONE:
                        break;
                    case EvseTestState.TEST_SELECT:
                        test_nr = 0;
                        foreach (var test_t in test[mtl.Id].test_type)
                        {
                            if (test_t.state == CurrentTestState.NONE)
                            {
                                switch (test_nr)//neleidziam vienu metu irangos naudoti kitam evse
                                {
                                    case TestType.HV_PRAMUSIMAS:
                                    case TestType.HV_ATSPARUMAS:
                                        for (int a = 0; a < 3; a++)
                                        {
                                            test[a].test_type[TestType.HV_PRAMUSIMAS].state = CurrentTestState.USING;
                                            test[a].test_type[TestType.HV_ATSPARUMAS].state = CurrentTestState.USING;
                                        }
                                        break;
                                    case TestType.EVSE_APKROVA:
                                        for (int a = 0; a < 3; a++)
                                        {
                                            test[a].test_type[TestType.EVSE_APKROVA].state = CurrentTestState.USING;
                                        }
                                        break;
                                    case TestType.WIFI:
                                    case TestType.GSM:
                                        for (int a = 0; a < 3; a++)
                                        {
                                            test[a].test_type[TestType.WIFI].state = CurrentTestState.USING;
                                            test[a].test_type[TestType.GSM].state = CurrentTestState.USING;
                                        }
                                        break;
                                }

                                test[mtl.Id].test_type[test_nr].state = CurrentTestState.PREPARE;
                                test[mtl.Id].all_tests_state = EvseTestState.TEST_STARTING;
                                test[mtl.Id].nr = test_nr;
                                break;
                            }
                            test_nr++;
                        }

                        break;
                    case EvseTestState.TEST_STARTING:
                        switch (test[mtl.Id].test_type[test[mtl.Id].nr].state)
                        {
                            case CurrentTestState.NONE:
                                System.Diagnostics.Debug.Print($"ERR_TEST_NONE:{0}");
                                break;
                            case CurrentTestState.USING:
                                System.Diagnostics.Debug.Print($"ERR_TEST_USING:{0}");
                                break;
                            case CurrentTestState.PREPARE:

                                y_pos = test[mtl.Id].test_type[test[mtl.Id].nr].y_position;


                                ProgressBar progress = new ProgressBar();
                                progress.Name = "progres" + i;
                                progress.Style = ProgressBarStyle.Continuous;
                                lbl_result_point = new Point(x: 20, y: y_pos + 5);
                                progress.Location = lbl_result_point;

                                btnStart.Visible = false;
                                lblWait.Visible = false;
                                panelTestResult.Controls.Add(progress);
                                lblLong.Text = Socket_.UnixTimeNow().ToString();

                                lblres[mtl.Id, test[mtl.Id].nr] = new Label { Name = "labelres" + mtl.testList[test[mtl.Id].nr].Id.ToString() };
                                lblres[mtl.Id, test[mtl.Id].nr].Location = lbl_result_point;
                                lblres[mtl.Id, test[mtl.Id].nr].Font = SymbolFont;
                                label[mtl.Id, test[mtl.Id].nr].Font = LargeFont;
                                label[mtl.Id, test[mtl.Id].nr].ForeColor = Color.Black;

                                switch (test[mtl.Id].nr)
                                {
                                    case TestType.HV_ATSPARUMAS:

                                        Tests_.hv_pramusimo_testas(mtl.Id);
                                        break;
                                    case TestType.HV_PRAMUSIMAS:
                                        break;
                                    case TestType.EVSE_APKROVA:
                                        break;
                                    case TestType.EVSE_KOMUNIKACIJA:
                                        break;
                                    case TestType.RCD:

                                        break;
                                    case TestType.WIFI:
                                        break;
                                    case TestType.GSM:
                                        break;
                                    case TestType.RFID:
                                        break;
                                }
                                break;
                            case CurrentTestState.TESTING:
                                break;
                            case CurrentTestState.HANDLE_RESULT:
                                break;
                            case CurrentTestState.DONE:
                                break;
                            default:
                                System.Diagnostics.Debug.Print($"ERR_test_unknown:{test[mtl.Id].test_type[test[mtl.Id].nr].state}");
                                break;

                        }
                        break;
                    case EvseTestState.TEST_IN_PROGRESS:
                        break;
                    case EvseTestState.TEST_FINISHED:
                        break;
                    case EvseTestState.ALL_TESTS_FINISHED:
                        break;

                }

                Thread.Sleep(500);
            }




            /*
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
                  }*/
        }

        void redraw_tests_lables()
        {
            int py = 10;
            int i = 1;
            int ptr = 1;
            /*lbl_ptr[0] = 1;
            lbl_ptr[1] = 1;
            lbl_ptr[2] = 1;
            lbl_ptr[3] = 1;
            lbl_ptr[4] = 1;*/

            mtl.testList = Helper.GettestLists();

            //Test_struc[] test = new Main.main.Test;

            foreach (var tst in mtl.testList)
            {
                //System.Diagnostics.Debug.Print($"START_BTN: = {0}");
                label[mtl.Id, ptr] = new Label { Name = "label" + tst.Id.ToString(), Text = tst.Name, AutoSize = true, Location = new Point(x: 60, y: py) };
                label[mtl.Id, ptr].Font = LargeFont;
                lblWorkplace[mtl.Id] = new Label { Name = "lblWorkplace" + tst.Id.ToString(), Text = (mtl.Id + 1).ToString(), AutoSize = true, Location = new Point(x: 35, y: 0), Font = new Font("Microsoft Sans Serif", 43), ForeColor = Color.Gray };
                label[mtl.Id, ptr].ForeColor = Color.Gray;
                lblWait = new Label { Name = "labelWait" + tst.Id.ToString() };
                lbl_result_point = new Point(x: 20, y: py + 5);
                lblWait.Location = lbl_result_point;
                lblWait.Font = SymbolFont;

                test[mtl.Id].test_type[i - 1].y_position = py;

                Action action = () =>
                {
                    lblWait.Text = ""; //WAIT symbol
                    panelTestResult.Controls.Add(label[mtl.Id, ptr]);
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

                //lbl_ptr[mtl.Id]++;
                ptr++;
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

        private void timer1_Tick(object sender, EventArgs e)
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
                System.Diagnostics.Debug.Print($"DevEvse: = NULL");
            }




            /*foreach (var monitor in mtl)
            {

            }*/




        }
    }
}
