using PS2000AImports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Ion.Sdk.Idi.InputPinAutoSequenceCommandParameter;

namespace ArtiluxEOL
{
    public partial class OscilloscopeTestWindow : Form
    {
        public OscilloscopeTestWindow()
        {
            InitializeComponent();
        }

        private void Exit_Oscilloscope_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Pico_Cancel_Click(object sender, EventArgs e)
        {
            Main.main.Cancel_Oscilloscope_Teset();
        }

        private void button_Time_Block_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Print("Run timer block= " + Main.main.RunTimerBlock());
        }

        private void button_Trigger_Block_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Print("Run trigger block= " + Main.main.RunTriggerBlock());
        }

        private void comboBox_Voltage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main.main.Input_Range_Selector(comboBox_Voltage.SelectedIndex);
        }

        private void comboBox_Channel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main.main.Channel_Selector(comboBox_Channel.SelectedIndex);
        }

        private void textBox_TimeBase_TextChanged(object sender, EventArgs e)
        {
            string input_str = textBox_TimeBase.Text;

            uint input_int = 1000;

            if (uint.TryParse(input_str, out input_int))
            {
                if (input_int > 0 && input_int <= 5000)
                {
                    Main.main.Set_Time_Base = input_int * 100;
                }
            }
        }

        private void textBox_Treashold_TextChanged(object sender, EventArgs e)
        {
            string input_str = textBox_Treashold.Text;

            short input_short = 2000;

            if (short.TryParse(input_str, out input_short))
            {
                if (input_short > 0 && input_short <= 20000)
                {
                    Main.main.Trigger_Threshold = input_short;
                }
            }
        }
    }
}
