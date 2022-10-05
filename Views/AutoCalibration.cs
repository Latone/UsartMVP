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
        //private Config Global.config;
        private Form _form;

        void OnReceiveData(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Config Update")
            {
                //Операция из другого потока (не Main) -> используем Invoke
                this.Invoke(new Action(() => UpdateBoxes()));
            }
        }
        public Config config
        {
            get
            {
                Global.config.REVS = revs_box.Text;
                Global.config.T_GAS = t_gas_box.Text;
                Global.config.T_RED = t_red_box.Text;
                Global.config.GAS_TIME = gas_time_box.Text;
                Global.config.PETROL_TIME = petrol_time_box.Text;
                Global.config.G_PRES = g_press_box.Text;
                Global.config.MAP = map_box.Text;

                var checkedRadioButton = rb_container.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);
                int h = checkedRadioButton.Name[checkedRadioButton.Name.Length - 1] - '0';
                Global.config.rb = (radio_button)h;

                return Global.config;
            }
            set
            {
                Global.config = value;
            }
        }
        public AutoCalibration(Form1 form)
        {
            InitializeComponent();
            _form = form;

            rb_container = new List<RadioButton>();
            rb_container.AddRange(new List<RadioButton>{
                radioButton1,
                radioButton2,
                radioButton3 }
            );

            revs_progressBar.Maximum = 6500;

            calibration_bar.Maximum = 100;

            UpdateBoxes();
        }
        void UpdateBoxes()
        {
            revs_progressBar.Value = (int)Double.Parse(Global.config.REVS, CultureInfo.InvariantCulture);
            revs_box.Text = Global.config.REVS;
            t_gas_box.Text = Global.config.T_GAS;
            t_red_box.Text = Global.config.T_RED;
            gas_time_box.Text = Global.config.GAS_TIME;
            petrol_time_box.Text = Global.config.PETROL_TIME;
            g_press_box.Text = Global.config.G_PRES;
            map_box.Text = Global.config.MAP;
            revs_rpm_box.Text = Global.config.REVS;

            string rbName = "";

            if ((int)Global.config.rb == 0)
                rbName = "radioButton1";
            else
                rbName = "radioButton" + ((int)Global.config.rb);

            RadioButton button = this.Controls.Find(rbName, true).FirstOrDefault() as RadioButton;
            button.Checked = true;
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

        private void AutoCalibration_Load(object sender, EventArgs e)
        {
            Global.StaticPropertyChanged += OnReceiveData;
        }

        private void AutoCalibration_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.StaticPropertyChanged -= OnReceiveData;
        }
    }
}
