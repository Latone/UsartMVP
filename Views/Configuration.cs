using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Views
{
    
    public partial class Configuration : Form, IManageConfigForm
    {
        private List<RadioButton> rb_container;
        private Config _config;
        private Form _form;

        public Config config {
            get {
                _config.REVS = revs_box.Text;
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

                return _config;
            }
            set {
                _config = value;

                revs_box.Text = _config.REVS;
                t_gas_box.Text = _config.T_GAS;
                t_red_box.Text = _config.T_RED;
                gas_time_box.Text = _config.GAS_TIME;
                petrol_time_box.Text = _config.PETROL_TIME;
                g_press_box.Text = _config.G_PRES;
                map_box.Text = _config.MAP;

                string rbName = "";

                if ((int)_config.rb == 0)
                    rbName = "radioButton1";
                else
                    rbName = "radioButton" + ((int)_config.rb);

                    RadioButton button = this.Controls.Find(rbName, true).FirstOrDefault() as RadioButton;
                    button.Checked = true;
                    //button.PerformClick();
                l_on_m_box.Text = _config.l_on_mazda;
                extra_inj_box.Checked = _config.inj_sens;
            }
        }

        public Configuration(Form1 form,Config mainConfig)
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
        }

        private void Configuration_FormClosed(object sender, FormClosedEventArgs e)
        {
            _form.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            revs_grid.AutoGenerateColumns = false;
            //Заполнение таблицы
            revs_grid.TopLeftHeaderCell.Value = "Times inj";
            //Rows
            this.revs_grid.Rows.Add("100", "101", "100", "100", "102", "101", "103", "104", "104", "103", "101", "101");
            this.revs_grid.Rows.Add("103", "104", "103", "103", "105", "105", "106", "105", "105", "104", "102", "102");
            this.revs_grid.Rows.Add("103", "104", "105", "105", "107", "109", "110", "109", "109", "108", "106", "106");
            this.revs_grid.Rows.Add("108", "109", "110", "111", "113", "115", "116", "115", "115", "114", "112", "112");
            this.revs_grid.Rows.Add("110", "112", "113", "114", "116", "118", "119", "118", "118", "117", "115", "115");
            this.revs_grid.Rows.Add("109", "114", "115", "116", "118", "120", "122", "121", "121", "120", "118", "118");
            this.revs_grid.Rows.Add("112", "116", "117", "117", "119", "121", "123", "122", "122", "121", "119", "119");
            this.revs_grid.Rows.Add("115", "117", "117", "117", "119", "121", "123", "122", "122", "121", "119", "119");
            this.revs_grid.Rows.Add("116", "118", "118", "117", "119", "121", "123", "122", "122", "121", "119", "119");
            this.revs_grid.Rows.Add("115", "118", "118", "117", "119", "121", "123", "122", "122", "121", "119", "119");
            this.revs_grid.Rows.Add("115", "118", "118", "117", "119", "121", "123", "122", "122", "121", "119", "119");
            this.revs_grid.Rows.Add("114", "117", "117", "116", "118", "120", "122", "121", "121", "120", "118", "118");

            //Header Rows
            revs_grid.Rows[0].HeaderCell.Value = "2,00";
            revs_grid.Rows[1].HeaderCell.Value = "2,50";
            revs_grid.Rows[2].HeaderCell.Value = "3,00";
            revs_grid.Rows[3].HeaderCell.Value = "3,50";
            revs_grid.Rows[4].HeaderCell.Value = "4,50";
            revs_grid.Rows[5].HeaderCell.Value = "6,00";
            revs_grid.Rows[6].HeaderCell.Value = "8,00";
            revs_grid.Rows[7].HeaderCell.Value = "10,00";
            revs_grid.Rows[8].HeaderCell.Value = "12,00";
            revs_grid.Rows[9].HeaderCell.Value = "14,00";
            revs_grid.Rows[10].HeaderCell.Value = "16,00";
            revs_grid.Rows[11].HeaderCell.Value = "18,00";


            revs_grid.CurrentCell = revs_grid.Rows[Int32.Parse(_config.TABLE_REVS_Row)].
                                                    Cells[Int32.Parse(_config.TABLE_REVS_Column)];

        }
        private void revs_grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex != -1)
                return;
            if (e.ColumnIndex > -1)
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void revs_grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex != -1)
                return;

            if (e.ColumnIndex>-1)
            {
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Left = revs_grid.AdvancedCellBorderStyle.Left;
            }
        }

        private void Configuration_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do You Want To Save Your Data", "Save", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                SaveConfig();
                
            }
        }
        void SaveConfig() {
            MockDataStore store = new MockDataStore();
            store.SaveToProperties(config);
        }
    }
}
