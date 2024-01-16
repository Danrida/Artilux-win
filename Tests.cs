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

    }
}
