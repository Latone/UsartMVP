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
    public partial class View : Form
    {
        private List<RadioButton> rb_container;
        //private Config Global.config;
        private Form _form;
        void OnReceiveData(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Config Update" && IsHandleCreated)
            {
                //Операция из другого потока (не Main) -> используем Invoke
                this.Invoke(new Action(() => UpdateBoxes()));
            }
        }
        public Config config
        {
            get
            {
                /*Global.config.REVS = revs_box.Text;
                Global.config.T_GAS = t_gas_box.Text;
                Global.config.T_RED = t_red_box.Text;
                Global.config.GAS_TIME = gas_time_box.Text;
                Global.config.PETROL_TIME = petrol_time_box.Text;
                Global.config.G_PRES = g_press_box.Text;
                Global.config.MAP = map_box.Text;

                Global.config.TABLE_REVS = revs_grid.CurrentCell.Value.ToString();
                Global.config.TABLE_REVS_Column = revs_grid.CurrentCell.ColumnIndex.ToString();
                Global.config.TABLE_REVS_Row = revs_grid.CurrentCell.RowIndex.ToString();


                var checkedRadioButton = rb_container.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);

                int h = checkedRadioButton.Name[checkedRadioButton.Name.Length - 1] - '0';
                Global.config.rb = (radio_button)h;
                Global.config.l_on_mazda = l_on_m_box.Text;
                Global.config.inj_sens = extra_inj_box.Checked;
*/
                return Global.config;
            }
            set
            {
                Global.config = value;
                
            }
        }
        void UpdateBoxes() {
            //textBoxes
            t_red_tb.Text = Global.config.T_RED;
            tg_tb.Text = Global.config.T_GAS;
            gp_tb.Text = Global.config.G_PRES;
            mp_tb.Text = Global.config.MAP;
            pi_tb.Text = Global.config.PETROL_TIME;
            gi_tb.Text = Global.config.GAS_TIME;

            //Bars
            revs_rpm_b.Value = (int)Double.Parse(Global.config.REVS, CultureInfo.InvariantCulture);
            t_red_b.Value = (int)Double.Parse(Global.config.T_RED, CultureInfo.InvariantCulture);
            t_gas_b.Value = (int)Double.Parse(Global.config.T_GAS, CultureInfo.InvariantCulture);
            g_pr_b.Value = (int)Double.Parse(Global.config.G_PRES, CultureInfo.InvariantCulture);
            m_pr_b.Value = (int)Double.Parse(Global.config.MAP, CultureInfo.InvariantCulture);
            p_inj_b.Value = (int)Double.Parse(Global.config.PETROL_TIME, CultureInfo.InvariantCulture);
            gas_inj_b.Value = (int)Double.Parse(Global.config.GAS_TIME, CultureInfo.InvariantCulture);

            string rbName = "";

            if ((int)Global.config.rb == 0)
                rbName = "radioButton1";
            else
                rbName = "radioButton" + ((int)Global.config.rb);

            RadioButton button = this.Controls.Find(rbName, true).FirstOrDefault() as RadioButton;
            button.Checked = true;
            //button.PerformClick();
            //l_on_m_box.Text = Global.config.l_on_mazda;
            //extra_inj_box.Checked = Global.config.inj_sens;
        }
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

        private void View_Load(object sender, EventArgs e)
        {
            Global.StaticPropertyChanged += OnReceiveData;
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.StaticPropertyChanged -= OnReceiveData;
        }
    }
}
