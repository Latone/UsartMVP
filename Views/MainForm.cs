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
using System.Threading;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class Form1 : LocalizedForm
    {
        //Main port for COM
        private SerialPort MyserialPort_;

        //Инициализация компонентов
        public Form1()
        {
            InitializeComponent();
            //Load from params
            MockDataStore store = new MockDataStore();
            var t = Task.Run(() => store.GetFromProperties());
            t.Wait();
            Global.config = t.Result;
        }

        //Обновлеине данных при отключении порта
        void UpdateDisconnectedView()
        {
            this.Invoke(new Action(() =>
            {
                //Отписка от ивента
                MyserialPort_.DataReceived -= new SerialDataReceivedEventHandler(ReceiveData.DataReceivedHandler);
                string cName = StaticLangManager.GlobalUICulture.Name;
                if (cName == "ru-RU")
                {
                    Connect_button.Text = "Подключиться";
                    conn_status.Text = "Отключено";
                }
                else {
                    Connect_button.Text = "Connect";
                    conn_status.Text = "Disconnected";
                }
                port_box.Enabled = true;
                update_ports.Enabled = true;
                

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

                if(cName=="ru-RU")
                    MessageBox.Show("COM-порт отключён");
                else
                    MessageBox.Show("COM-port disconnected");
            }));
        }

        //Метод на обработку данных по вызову. См Model.Global.cs
        void OnReceiveData(object sender, PropertyChangedEventArgs e)
        {
            string cName = StaticLangManager.GlobalUICulture.Name;
            //Обновление данных в GUI (дебаг)
            if (e.PropertyName == "Config Update" && IsHandleCreated)
            {
                //Операция из другого потока (не Main) -> используем Invoke
                this.Invoke(new Action(() =>
                {
                    Config_status_1.Text = Global.config.getBasicInfo1();
                    Config_status_2.Text = Global.config.getBasicInfo2();
                }));
            }

            //Обработка отключения
            if (e.PropertyName == "COM Disconnected" && IsHandleCreated)
            {
                //Обычное отключение по кнопке
                if ((conn_status.Text == "Отключено" || conn_status.Text == "Disconnected") && !Global.config.connection_error)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        UpdateDisconnectedView();
                        Global.config.connection_error = false;
                    }));
                    return;
                }
                //Переподключение
                //3 попытки, интервал 1 сек.
                if ((conn_status.Text != "Отключено" || conn_status.Text == "Disconnected") && Global.config.connection_error)
                    try
                    {

                        if (!MyserialPort_.IsOpen)
                        {
                            Retry.Do(() => MyserialPort_.Open(), TimeSpan.FromSeconds(1));
                            Global.config.connection_error = false;
                            if(cName == "ru-RU")
                                MessageBox.Show("Порт переподключен");
                            else
                                MessageBox.Show("Port reconnected");
                        }
                    }
                    catch (Exception ex)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            //throw new Exception(ex.Message);
                            if (cName == "ru-RU")
                                MessageBox.Show("Ошибка переподключения\n" + ex.Message);
                            else
                                MessageBox.Show("Reconnection error occured\n" + ex.Message);
                            Global.config.connection_error = false;
                            UpdateDisconnectedView();
                        }));
                    }
            }
        }

        //Подключение по клику кнопки
        private void Connect_button_Click(object sender, EventArgs e)
        {
            string cName = StaticLangManager.GlobalUICulture.Name;
            if (cName=="ru-RU" && Connect_button.Text == "Подключиться" ||
                (cName == "en-US" && Connect_button.Text == "Connect"))
            {
                try
                {   //выбор порта'
                    MyserialPort_.PortName = port_box.Text;
                    //Хэндл ивента на получение данных
                    MyserialPort_.DataReceived += new SerialDataReceivedEventHandler(ReceiveData.DataReceivedHandler);


                    //открытие
                    //3 попытки, интервал 1 сек.
                    Retry.Do(() => MyserialPort_.Open(), TimeSpan.FromSeconds(1));

                    //элемент не действителен
                    port_box.Enabled = false;
                    update_ports.Enabled = false;

                    if (cName == "en-US") {
                        Connect_button.Text = "Disconnect";
                        conn_status.Text = "Connected to port " + port_box.Text;

                    }
                    else
                    {
                        Connect_button.Text = "Отключиться";
                        conn_status.Text = "Подключено к порту " + port_box.Text;
                    }
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message);
                    if (cName == "en-US")
                    {
                        MessageBox.Show("Connection Error\n" + ex.Message);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка подключения\n" + ex.Message);
                    }
                }
            }
            else if (cName == "ru-RU" && Connect_button.Text == "Отключиться" ||
                (cName == "en-US" && Connect_button.Text == "Disconnect"))
            {
                if(cName == "ru-RU")
                    conn_status.Text = "Отключено";
                else
                    conn_status.Text = "Disconnected";
                try
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        MyserialPort_.Close();
                    }));
                    Global.COMdisconnect();
                }
                catch (Exception ex)
                {
                    //Global.COMdisconnect();
                }

                port_box.Enabled = true;
                update_ports.Enabled = true;
                //((Lambda_meter)this.Tag).comboBoxPorts.Text = comboBoxPorts_.Text;
                if (cName == "ru-RU")
                    Connect_button.Text = "Подключиться";
                else
                    Connect_button.Text = "Connect";
                //
            }

        }

        //Переход в окно конфига
        private void button_config_Click(object sender, EventArgs e)
        {
            using (Configuration window = new Configuration(this))
            {

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
        void updports()
        {
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

        private void save_butt_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\";
            sfd.Title = "Save text Files";
            sfd.CheckFileExists = false;
            sfd.CheckPathExists = true;
            sfd.DefaultExt = "ini";
            sfd.Filter = "Initialization files (*.ini)|*.ini|All files (*.*)|*.*";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try { 
                var MyIni = new IniFile(sfd.FileName);

                MyIni.Write("REVS", Global.config.REVS, "Common");
                MyIni.Write("MAP", Global.config.MAP, "Common");
                MyIni.Write("G pressure", Global.config.G_PRES, "Common");
                MyIni.Write("Injection sens", Global.config.inj_sens.ToString(), "Common");
                MyIni.Write("L on Mazda", Global.config.l_on_mazda.ToString(), "Common");

                MyIni.Write("Gas temperature", Global.config.T_GAS, "Temp C");
                MyIni.Write("Red temperature", Global.config.T_RED, "Temp C");
                MyIni.Write("Air temperature", Global.config.T_AIR, "Temp C");

                MyIni.Write("Gas time", Global.config.GAS_TIME, "Time");
                MyIni.Write("Petrol time", Global.config.PETROL_TIME, "Time");

                MyIni.Write("time_1", Global.config.time_1, "Extra");
                MyIni.Write("time_2", Global.config.time_2, "Extra");
                MyIni.Write("time_3", Global.config.time_3.ToString(), "Extra");
                MyIni.Write("time_4", Global.config.time_4.ToString(), "Extra");
                MyIni.Write("Ticking time (0-200)", Global.config.test_time.ToString(), "Extra");
                }
                    catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при записи файла:\n" + ex.Message);
                }
            }
        }

        private void load_butt_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.Title = "Save text Files";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = "ini";
            ofd.Filter = "Initialization files (*.ini)|*.ini|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var MyIni = new IniFile(ofd.FileName);

                    Global.config.REVS = MyIni.Read("REVS");
                    Global.config.MAP = MyIni.Read("MAP");
                    Global.config.G_PRES = MyIni.Read("G pressure");
                    Global.config.inj_sens = Convert.ToBoolean(MyIni.Read("Injection sens").ToLower());
                    Global.config.l_on_mazda = MyIni.Read("L on Mazda");

                    Global.config.T_GAS = MyIni.Read("Gas temperature");
                    Global.config.T_RED = MyIni.Read("Red temperature");
                    Global.config.T_AIR = MyIni.Read("Air temperature");

                    Global.config.GAS_TIME = MyIni.Read("Gas time");
                    Global.config.PETROL_TIME = MyIni.Read("Petrol time");

                    Global.config.time_1 = MyIni.Read("time_1");
                    Global.config.time_2 = MyIni.Read("time_2");
                    Global.config.time_3 = MyIni.Read("time_3");
                    Global.config.time_4 = MyIni.Read("time_4");
                    Global.config.test_time = MyIni.Read("Ticking time (0-200)");

                    Global.updateConfig();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии файла:\n" + ex.Message);
                }
            }
        }
        
        private void ch_lang_Click(object sender, EventArgs e)
        {
            
            using (Language window = new Language())
            {
                if (window.ShowDialog() == DialogResult.OK) { }

            }
        }
    }
}
