using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Views
{
    public partial class AutoCalibration : Form
    {
        private List<RadioButton> rb_container;
        private Config _config;
        private Form _form;
        public Config config
        {
            get
            {
                _config.REVS = revs_box.Text;
                _config.T_GAS = t_gas_box.Text;
                _config.T_RED = t_red_box.Text;
                _config.GAS_TIME = gas_time_box.Text;
                _config.PETROL_TIME = petrol_time_box.Text;
                _config.G_PRES = g_press_box.Text;
                _config.MAP = map_box.Text;

                var checkedRadioButton = rb_container.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);
                int h = checkedRadioButton.Name[checkedRadioButton.Name.Length - 1] - '0';
                _config.rb = (radio_button)h;

                return _config;
            }
            set
            {
                _config = value;

                revs_box.Text = _config.REVS;
                t_gas_box.Text = _config.T_GAS;
                t_red_box.Text = _config.T_RED;
                gas_time_box.Text = _config.GAS_TIME;
                petrol_time_box.Text = _config.PETROL_TIME;
                g_press_box.Text = _config.G_PRES;
                map_box.Text = _config.MAP;
                revs_rpm_box.Text = _config.REVS;
                
                string rbName = "";

                if ((int)_config.rb == 0)
                    rbName = "radioButton1";
                else
                    rbName = "radioButton" + ((int)_config.rb);

                RadioButton button = this.Controls.Find(rbName, true).FirstOrDefault() as RadioButton;
                button.Checked = true;
            }
        }
        public AutoCalibration(Form1 form, Config mainConfig)
        {
            InitializeComponent();
            _form = form;
            config = mainConfig;

            rb_container = new List<RadioButton>();
            rb_container.AddRange(new List<RadioButton>{
                radioButton1,
                radioButton2,
                radioButton3 }
            );

            revs_progressBar.Maximum = 6500;
            
            revs_progressBar.Value = (int)Double.Parse(_config.REVS, CultureInfo.InvariantCulture);

            calibration_bar.Maximum = 100;
        }

        private void calibr_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                calibration_bar.Value = 0;
                calibr.Text = "Start calibration";
            }
            else
            {
                timer1.Start();
                calibr.Text = "Stop calibration";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (calibration_bar.Value < 100)
                calibration_bar.Value += 1;
            else if (calibration_bar.Value == 100)
            {
                timer1.Stop();

                DialogResult dialogResult = MessageBox.Show("Calibration Complete", "Save", MessageBoxButtons.OK);
                if (dialogResult == DialogResult.Yes)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                }
                
                calibration_bar.Value = 0;
                calibr.Text = "Start calibration";
            }
        }
    }
}
