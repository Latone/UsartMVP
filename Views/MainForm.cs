using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Views;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Services;
using WindowsFormsApp1.COM;
using System.IO.Ports;
using System.Globalization;
using View = WindowsFormsApp1.Views.View;
using System.Reflection;
using System.ComponentModel;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        //Main config
        //public Config config { get; private set; }
        //Main port for COM
        private SerialPort MyserialPort_;

        public Form1()
        {
            InitializeComponent();
            //Load from params
            MockDataStore store = new MockDataStore();
            var t = Task.Run(()=> store.GetFromProperties());
            t.Wait();
            Global.config = t.Result;
        }
        void OnReceiveData(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Config Update")
            {
                //Операция из другого потока (не Main) -> используем Invoke
                this.Invoke(new Action(() => Config_status.Text = Global.config.toString()));
            }
        }
                private void Connect_button_Click(object sender, EventArgs e)
        {
            if (Connect_button.Text == "Подключиться")
            {
                try
                {   //выбор порта
                    MyserialPort_.PortName = port_Box.Text;
                    //Хэндл ивента на получение данных
                    MyserialPort_.DataReceived += new SerialDataReceivedEventHandler(ReceiveData.DataReceivedHandler);
                    
                    
                    //открытие
                    //3 попытки, интервал 1 сек.
                    Retry.Do(() => MyserialPort_.Open(), TimeSpan.FromSeconds(1));

                    //элемент не действителен
                    port_Box.Enabled = false;

                    //((Lambda_meter)this.Tag).comboBoxPorts.Text = comboBoxPorts_.Text;
                    Connect_button.Text = "Отключиться";
                    conn_status.Text = "Подключено к порту " + port_Box.Text;
                }
                catch(Exception ex)
                {
                    //throw new Exception(ex.Message);
                    MessageBox.Show("Ошибка подключения\n"+ex.Message);
                }
            }
            else if (Connect_button.Text == "Отключиться")
            {
                MyserialPort_.Close();
                port_Box.Enabled = true;
                //((Lambda_meter)this.Tag).comboBoxPorts.Text = comboBoxPorts_.Text;
                Connect_button.Text = "Подключиться";
                conn_status.Text = "Отключено";
            }

        }

        private void button_config_Click(object sender, EventArgs e)
        {
            using (Configuration window = new Configuration(this)) {
                //this.Enabled = false;
                //window.Show();

                if (window.ShowDialog() == DialogResult.OK) { }
                   // this.config = window.config;

            }
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //default COM to use
            MyserialPort_ = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            //Subscribe (Only once, 'cos it's global) to update the values in Main form:
            Global.StaticPropertyChanged += OnReceiveData;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Config_status.Text = Global.config.toString();
        }


        private void button_calibration_Click(object sender, EventArgs e)
        {
            if (Math.Round(Double.Parse(Global.config.REVS, CultureInfo.InvariantCulture)) > 6500)
            {
                DialogResult dialogResult = MessageBox.Show("REVS is out of bound (6500 max)", "Warning", MessageBoxButtons.OK);
                if (dialogResult == DialogResult.OK)
                {
                    return;
                }
            }
            using (AutoCalibration window = new AutoCalibration(this))
            {
                //this.Enabled = false;
                //window.Show();

                if (window.ShowDialog() == DialogResult.OK) { }
                //    this.config = window.config;

            }
        }

        private void view_Click(object sender, EventArgs e)
        {
            using (View window = new View(this))
            {
                //this.Enabled = false;
                //window.Show();

                if (window.ShowDialog() == DialogResult.OK) { }
                    //this.config = window.config;

            }
        }
    }
}
