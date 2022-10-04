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
        private Config _config;
        private Form _form;

        public Config config
        {
            get
            {
                /*_config.REVS = revs_box.Text;
                _config.T_GAS = t_gas_box.Text;
                _config.T_RED = t_red_box.Text;
                _config.GAS_TIME = gas_time_box.Text;
                _config.PETROL_TIME = petrol_time_box.Text;
                _config.G_PRES = g_press_box.Text;
                _config.MAP = map_box.Text;

                _config.TABLE_REVS = revs_grid.CurrentCell.Value.ToString();
                _config.TABLE_REVS_Column = revs_grid.CurrentCell.ColumnIndex.ToString();
                _config.TABLE_REVS_Row = revs_grid.CurrentCell.RowIndex.ToString();


                var checkedRadioButton = rb_container.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);

                int h = checkedRadioButton.Name[checkedRadioButton.Name.Length - 1] - '0';
                _config.rb = (radio_button)h;
                _config.l_on_mazda = l_on_m_box.Text;
                _config.inj_sens = extra_inj_box.Checked;
*/
                return _config;
            }
            set
            {
                _config = value;
                //textBoxes
                t_red_tb.Text = _config.T_RED;
                tg_tb.Text = _config.T_GAS;
                gp_tb.Text = _config.G_PRES;
                mp_tb.Text = _config.MAP;
                pi_tb.Text = _config.PETROL_TIME;
                gi_tb.Text = _config.GAS_TIME;

                //Bars
                revs_rpm_b.Value = (int)Double.Parse(_config.REVS, CultureInfo.InvariantCulture);
                t_red_b.Value = (int)Double.Parse(_config.T_RED, CultureInfo.InvariantCulture);
                t_gas_b.Value = (int)Double.Parse(_config.T_GAS, CultureInfo.InvariantCulture);
                g_pr_b.Value = (int)Double.Parse(_config.G_PRES, CultureInfo.InvariantCulture);
                m_pr_b.Value = (int)Double.Parse(_config.MAP, CultureInfo.InvariantCulture);
                p_inj_b.Value = (int)Double.Parse(_config.PETROL_TIME, CultureInfo.InvariantCulture);
                gas_inj_b.Value = (int)Double.Parse(_config.GAS_TIME, CultureInfo.InvariantCulture);

                string rbName = "";

                if ((int)_config.rb == 0)
                    rbName = "radioButton1";
                else
                    rbName = "radioButton" + ((int)_config.rb);

                RadioButton button = this.Controls.Find(rbName, true).FirstOrDefault() as RadioButton;
                button.Checked = true;
                //button.PerformClick();
                //l_on_m_box.Text = _config.l_on_mazda;
                //extra_inj_box.Checked = _config.inj_sens;
            }
        }

        public View(Form1 form, Config mainConfig)
        {
            InitializeComponent();

            rb_container = new List<RadioButton>();
            rb_container.AddRange(new List<RadioButton>{
                radioButton1,
                radioButton2,
                radioButton3 }
            );

            _form = form;
            config = mainConfig;
        }
    }
}
