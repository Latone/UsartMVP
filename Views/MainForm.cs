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
        void UpdateDisconnectedView() {
            MyserialPort_.DataReceived -= new SerialDataReceivedEventHandler(ReceiveData.DataReceivedHandler);

            Connect_button.Text = "Подключиться";
            port_box.Enabled = true;
            update_ports.Enabled = true;
            conn_status.Text = "Отключено";

            Global.config.REVS = "0";
            Global.config.T_GAS = "0";
            Global.config.T_RED = "0";
            Global.config.GAS_TIME = "0";

            Global.updateConfig();

            MessageBox.Show("COM-порт отключён");
        }
        void OnReceiveData(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Config Update")
            {
                //Операция из другого потока (не Main) -> используем Invoke
                this.Invoke(new Action(() => Config_status.Text = Global.config.getBasicInfo()));
            }
            if (e.PropertyName == "COM Disconnected")
            {
                //Обычное отключение по кнопке
                if (conn_status.Text == "Отключено")
                {
                    this.BeginInvoke(new Action(() => {
                        UpdateDisconnectedView();
                    }));
                    return;
                }    
                //Переподключение
                //3 попытки, интервал 1 сек.
                try
                {
                    if (!MyserialPort_.IsOpen)
                    {
                        Retry.Do(() => MyserialPort_.Open(), TimeSpan.FromSeconds(1));
                        MessageBox.Show("Порт переподключен");
                    }
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message);
                    MessageBox.Show("Ошибка переподключения\n" + ex.Message);
                

                    this.BeginInvoke(new Action(() => {
                        UpdateDisconnectedView();
                    }));
                }
            }
        }
                private void Connect_button_Click(object sender, EventArgs e)
        {
            if (Connect_button.Text == "Подключиться")
            {
                try
                {   //выбор порта
                    //if(!MyserialPort_.IsOpen)
                    MyserialPort_.PortName = port_box.Text;
                    //Хэндл ивента на получение данных
                    MyserialPort_.DataReceived += new SerialDataReceivedEventHandler(ReceiveData.DataReceivedHandler);
                    
                    
                    //открытие
                    //3 попытки, интервал 1 сек.
                    Retry.Do(() => MyserialPort_.Open(), TimeSpan.FromSeconds(1));

                    //элемент не действителен
                    port_box.Enabled = false;
                    update_ports.Enabled = false;
                    //((Lambda_meter)this.Tag).comboBoxPorts.Text = comboBoxPorts_.Text;
                    Connect_button.Text = "Отключиться";
                    conn_status.Text = "Подключено к порту " + port_box.Text;
                }
                catch(Exception ex)
                {
                    //throw new Exception(ex.Message);
                    MessageBox.Show("Ошибка подключения\n"+ex.Message);
                }
            }
            else if (Connect_button.Text == "Отключиться")
            {
                conn_status.Text = "Отключено";
                MyserialPort_.Close();

                port_box.Enabled = true;
                update_ports.Enabled = true;
                //((Lambda_meter)this.Tag).comboBoxPorts.Text = comboBoxPorts_.Text;
                Connect_button.Text = "Подключиться";
                
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
            updports();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Config_status.Text = Global.config.getBasicInfo();
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

        private void update_ports_Click(object sender, EventArgs e)
        {
            updports();
        }
        void updports() {
            string[] ports = SerialPort.GetPortNames();
            port_box.Text = "";
            port_box.Items.Clear();
            if (ports.Length != 0)
            {
                port_box.Items.AddRange(ports);
                port_box.SelectedIndex = 0;
            }
        }
    }
}
