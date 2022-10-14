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
    /// <summary>
    /// Форма просмотра
    /// </summary>
    public partial class View : Form
    {
        private List<RadioButton> rb_container;
        private Form _form;

        //Подписанный метод
        void OnReceiveData(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Config Update" && IsHandleCreated)
            {
                //Операция из другого потока (не Main) -> используем Invoke
                this.Invoke(new Action(() => UpdateBoxes()));
            }
        }
        //Локальный GET;SET;
        public Config config
        {
            get
            {
                return Global.config;
            }
            set
            {
                Global.config = value;
                
            }
        }
        //Обработка загружаемых данных на вывод в GUI
        void UpdateBoxes() {
            //textBoxes
            t_red_tb.Text = Global.config.T_RED;
            tg_tb.Text = Global.config.T_GAS;
            gp_tb.Text = Global.config.G_PRES;
            mp_tb.Text = Global.config.MAP;
            pi_tb.Text = Global.config.PETROL_TIME;
            gi_tb.Text = Global.config.GAS_TIME;
            air_box.Text = Global.config.T_AIR;
            //Bars
            revs_rpm_b.Value = (int)Double.Parse(Global.config.REVS, CultureInfo.InvariantCulture);
            t_red_b.Value = (int)Double.Parse(Global.config.T_RED, CultureInfo.InvariantCulture);
            t_gas_b.Value = (int)Double.Parse(Global.config.T_GAS, CultureInfo.InvariantCulture);
            g_pr_b.Value = (int)Double.Parse(Global.config.G_PRES, CultureInfo.InvariantCulture);
            m_pr_b.Value = (int)Double.Parse(Global.config.MAP, CultureInfo.InvariantCulture);
            p_inj_b.Value = (int)Double.Parse(Global.config.PETROL_TIME, CultureInfo.InvariantCulture);
            gas_inj_b.Value = (int)Double.Parse(Global.config.GAS_TIME, CultureInfo.InvariantCulture);
            air_bar.Value = (int)Double.Parse(Global.config.T_AIR, CultureInfo.InvariantCulture);
            string rbName = "";

            if ((int)Global.config.rb == 0)
                rbName = "radioButton1";
            else
                rbName = "radioButton" + ((int)Global.config.rb);

            RadioButton button = this.Controls.Find(rbName, true).FirstOrDefault() as RadioButton;
            button.Checked = true;
        }
        //Конструктор
        //Загрузка данных из общего конфига
        public View(Form1 form)
        {
            InitializeComponent();

            rb_container = new List<RadioButton>();
            rb_container.AddRange(new List<RadioButton>{
                radioButton1,
                radioButton2,
                radioButton3 }
            );

            _form = form;
            UpdateBoxes();
        }

        //Ивент при загрузке (подписка на изменение/вызов)
        private void View_Load(object sender, EventArgs e)
        {
            Global.StaticPropertyChanged += OnReceiveData;
        }

        //Ивент при закрытии (отписка от изменения/вызова)
        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.StaticPropertyChanged -= OnReceiveData;
        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tg_tb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
