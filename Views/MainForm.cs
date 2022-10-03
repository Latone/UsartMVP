using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Views;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Config config { get; private set; }
        public Form1()
        {
            InitializeComponent();
            //Load from params
            MockDataStore store = new MockDataStore();
            var t = Task.Run(()=> store.GetFromProperties());
            t.Wait();
            config = t.Result;
        }

        private void Connect_button_Click(object sender, EventArgs e)
        {
            /*if (buttonConnect_.Text == "Подключится")
            {
                try
                {
                    //выбор порта
                    MyserialPort_.PortName = comboBoxPorts_.Text;
                    //открытие
                    MyserialPort_.Open();
                    //элемент не действителен
                    comboBoxPorts_.Enabled = false;
                    ((Lambda_meter)this.Tag).comboBoxPorts.Text = comboBoxPorts_.Text;
                    buttonConnect_.Text = "Отключится";
                }
                catch
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }
            else if (buttonConnect_.Text == "Отключится")
            {
                MyserialPort_.Close();
                comboBoxPorts_.Enabled = true;
                ((Lambda_meter)this.Tag).comboBoxPorts.Text = comboBoxPorts_.Text;
                buttonConnect_.Text = "Подключится";
            }*/

        }

        private void button_config_Click(object sender, EventArgs e)
        {
            using (Configuration window = new Configuration(this,config)) {
                //this.Enabled = false;
                //window.Show();

                if (window.ShowDialog() == DialogResult.OK)
                    this.config = window.config;

            }
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Config_status.Text = config.toString();
        }


        private void button_calibration_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(config.REVS) > 6500)
            {
                DialogResult dialogResult = MessageBox.Show("REVS is out of bound (6500 max)", "Warning", MessageBoxButtons.OK);
                if (dialogResult == DialogResult.OK)
                {
                    return;
                }
            }
            using (AutoCalibration window = new AutoCalibration(this, config))
            {
                //this.Enabled = false;
                //window.Show();

                if (window.ShowDialog() == DialogResult.OK)
                    this.config = window.config;

            }
        }
    }
}
