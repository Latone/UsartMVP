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
using System.ComponentModel;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class Form1 : Form
    {
        //Main port for COM
        private SerialPort MyserialPort_;

        //Инициализация компонентов
        public Form1()
        {
            InitializeComponent();
            //Load from params
            MockDataStore store = new MockDataStore();
            var t = Task.Run(()=> store.GetFromProperties());
            t.Wait();
            Global.config = t.Result;
        }
        
        //Обновлеине данных при отключении порта
        void UpdateDisconnectedView() {
            this.Invoke(new Action(() => {
                //Отписка от ивента
                MyserialPort_.DataReceived -= new SerialDataReceivedEventHandler(ReceiveData.DataReceivedHandler);

            Connect_button.Text = "Подключиться";
            port_box.Enabled = true;
            update_ports.Enabled = true;
            conn_status.Text = "Отключено";

            Global.config.REVS = "0";
            Global.config.T_GAS = "0";
            Global.config.T_RED = "0";
            Global.config.GAS_TIME = "0";

            Global.config.test_pressure = "0";
            Global.config.T_AIR = "0";
            Global.config.G_PRES = "0";
            Global.config.time_1 = "0";
            Global.config.time_2 = "0";
            Global.config.time_3 = "0";
            Global.config.time_4 = "0";
            Global.config.test_time = "0";
            Global.config.MAP = "0";
            Global.config.PETROL_TIME = "0";

            Global.updateConfig();

            MessageBox.Show("COM-порт отключён");
            }));
        }

        //Метод на обработку данных по вызову. См Model.Global.cs
        void OnReceiveData(object sender, PropertyChangedEventArgs e)
        {
            //Обновление данных в GUI (дебаг)
            if (e.PropertyName == "Config Update" && IsHandleCreated)
            {
                //Операция из другого потока (не Main) -> используем Invoke
                this.Invoke(new Action(() => { 
                    Config_status_1.Text = Global.config.getBasicInfo1();
                    Config_status_2.Text = Global.config.getBasicInfo2();
                }));
            }

            //Обработка отключения
            if (e.PropertyName == "COM Disconnected" && IsHandleCreated)
            {
                //Обычное отключение по кнопке
                if (conn_status.Text == "Отключено" && !Global.config.connection_error)
                {
                    this.BeginInvoke(new Action(() => {
                        UpdateDisconnectedView();
                        Global.config.connection_error = false;
                    }));
                    return;
                }
                //Переподключение
                //3 попытки, интервал 1 сек.
                if (conn_status.Text != "Отключено" && Global.config.connection_error)
                    try
                {

                    if (!MyserialPort_.IsOpen)
                    {
                        Retry.Do(() => MyserialPort_.Open(), TimeSpan.FromSeconds(1));
                        Global.config.connection_error = false;
                        MessageBox.Show("Порт переподключен");
                    }
                }
                catch (Exception ex)
                {
                        this.BeginInvoke(new Action(() => {
                            //throw new Exception(ex.Message);
                            MessageBox.Show("Ошибка переподключения\n" + ex.Message);
                            Global.config.connection_error = false;
                            UpdateDisconnectedView();
                        }));
                }
            }
        }

        //Подключение по клику кнопки
                private void Connect_button_Click(object sender, EventArgs e)
        {
            if (Connect_button.Text == "Подключиться")
            {
                try
                {   //выбор порта
                    MyserialPort_.PortName = port_box.Text;
                    //Хэндл ивента на получение данных
                    MyserialPort_.DataReceived += new SerialDataReceivedEventHandler(ReceiveData.DataReceivedHandler);
                    
                    
                    //открытие
                    //3 попытки, интервал 1 сек.
                    Retry.Do(() => MyserialPort_.Open(), TimeSpan.FromSeconds(1));

                    //элемент не действителен
                    port_box.Enabled = false;
                    update_ports.Enabled = false;

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
                try
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        MyserialPort_.Close();
                    }));
                    Global.COMdisconnect();
                }
                catch (Exception ex) {
                    //Global.COMdisconnect();
                }

                port_box.Enabled = true;
                update_ports.Enabled = true;
                //((Lambda_meter)this.Tag).comboBoxPorts.Text = comboBoxPorts_.Text;
                Connect_button.Text = "Подключиться";
                //
            }

        }

        //Переход в окно конфига
        private void button_config_Click(object sender, EventArgs e)
        {
            using (Configuration window = new Configuration(this)) {

                if (window.ShowDialog() == DialogResult.OK) { }

            }
                
        }

        //Ивент на обработку при загрузке формы
        private void Form1_Load(object sender, EventArgs e)
        {
            //default COM to use
            MyserialPort_ = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            //Subscribe (Only once, 'cos it's global) to update the values in Main form:
            Global.StaticPropertyChanged += OnReceiveData;
            updports();
        }

        //Ивент при активации формы
        private void Form1_Activated(object sender, EventArgs e)
        {
            Config_status_1.Text = Global.config.getBasicInfo1();
            Config_status_2.Text = Global.config.getBasicInfo2();
        }

        //Переход в окно калибровки
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
                if (window.ShowDialog() == DialogResult.OK) { }

            }
        }

        //Переход в окно просмотра
        private void view_Click(object sender, EventArgs e)
        {
            using (View window = new View(this))
            {
                if (window.ShowDialog() == DialogResult.OK) { }

            }
        }

        //Кнопка обновления списка портов
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

        //Переход в окно графика
        private void button8_Click(object sender, EventArgs e)
        {
            using (Plot3D.Graph3DMainForm window = new Plot3D.Graph3DMainForm())
            {
                if (window.ShowDialog() == DialogResult.OK) { }

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsHandleCreated)
                e.Cancel = true;

        }
    }
}
