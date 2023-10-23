using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtiluxEOL
{
    public partial class Utils : Form
    {
        public Utils()
        {
            InitializeComponent();
        }

        private Main mainForm = null;

        public Utils(Form callingForm)
        {
            //container.Add(this);
            mainForm = callingForm as Main;


            InitializeComponent();
            
        }
    }
}
