using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtiluxEOL
{
    public partial class DevControl : Component
    {
        public DevControl()
        {
            InitializeComponent();
        }

        public DevControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
