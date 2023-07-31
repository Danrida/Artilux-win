using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtiluxEOL
{
    public partial class Popup_msg : Form
    {
        public Popup_msg(String title, String msg, Color bgColor, int time_sec)
        {
            InitializeComponent();

           
            this.BackColor = bgColor;
            this.msg_title.Text = title;
            this.msg_text.Text = msg;
        }

        private void Popup_msg_Load(object sender, EventArgs e)
        {
            Top = 20;
            Left = Screen.PrimaryScreen.Bounds.Width - Width - 20;
            timerPopup.Start();
        }

        private void timerPopup_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
