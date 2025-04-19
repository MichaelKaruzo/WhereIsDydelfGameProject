using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereIsDydelfGameProject
{
    public partial class SettingsControlPanel : Form
    {
        private GameSet Setting; // Makin sure to get the values that are default
        public SettingsControlPanel(GameSet Set)
        {
            InitializeComponent();
            Setting = Set;
            SettingControl();
        }
        // Main Control Panel of settings
        private void SettingControl()
        {
            // Settin up values for certain fields ( aka. X = Width etc. )

            numericUpDown1.Value = Setting.Width;
            numericUpDown2.Value = Setting.Height;
            numericUpDown3.Value = Setting.Dydelfs;
            numericUpDown4.Value = Setting.Crocs;
            numericUpDown5.Value = Setting.Time;
            numericUpDown6.Value = Setting.Raccoon;

            // Setting up max/min Values for fields So for example X min is 3 and max 10 in the field.

            numericUpDown1.Minimum = 3;
            numericUpDown1.Maximum = 10;

            numericUpDown2.Minimum = 3;
            numericUpDown2.Maximum = 10;

            numericUpDown3.Minimum = 1;
            numericUpDown3.Maximum = 6;

            numericUpDown4.Minimum = 0;
            numericUpDown4.Maximum = 1;

            numericUpDown5.Minimum = 10;
            numericUpDown5.Maximum = 60;

            numericUpDown6.Minimum = 3;
            numericUpDown6.Maximum = 8;

            //Updatin values irt. ( In Real Time )

            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            numericUpDown3.ValueChanged += numericUpDown3_ValueChanged;
            numericUpDown4.ValueChanged += numericUpDown4_ValueChanged;
            numericUpDown5.ValueChanged += numericUpDown5_ValueChanged;
            numericUpDown6.ValueChanged += numericUpDown6_ValueChanged;

        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Setting.Width = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Setting.Height = (int)numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Setting.Dydelfs = (int)numericUpDown3.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            Setting.Crocs = (int)numericUpDown4.Value;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            Setting.Time = (int)numericUpDown5.Value;
        }


        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            Setting.Raccoon = (int)numericUpDown6.Value;
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

    }
}
