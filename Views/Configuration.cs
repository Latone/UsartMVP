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
        public Config config {
            get {
                Global.config.REVS = revs_box.Text;
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

                return Global.config;
            }
            set {
                Global.config = value;

                
            }
        }

        public Configuration(Form1 form)
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
        void UpdateBoxes() {

            revs_box.Text = Global.config.REVS;
            t_gas_box.Text = Global.config.T_GAS;
            t_red_box.Text = Global.config.T_RED;
            gas_time_box.Text = Global.config.GAS_TIME;
            petrol_time_box.Text = Global.config.PETROL_TIME;
            g_press_box.Text = Global.config.G_PRES;
            map_box.Text = Global.config.MAP;
            ait_box.Text = Global.config.T_AIR;
            test__count_bar.Value = Int32.Parse(Global.config.test_time);

            string rbName = "";

            if ((int)Global.config.rb == 0)
                rbName = "radioButton1";
            else
                rbName = "radioButton" + ((int)Global.config.rb);

            RadioButton button = this.Controls.Find(rbName, true).FirstOrDefault() as RadioButton;
            button.Checked = true;
            //button.PerformClick();
            l_on_m_box.Text = Global.config.l_on_mazda;
            extra_inj_box.Checked = Global.config.inj_sens;
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


            revs_grid.CurrentCell = revs_grid.Rows[Int32.Parse(Global.config.TABLE_REVS_Row)].
                                                    Cells[Int32.Parse(Global.config.TABLE_REVS_Column)];

            
            trackBar1.Maximum = 100;
            trackBar1.Value = Global.config.track_bar;

            Global.StaticPropertyChanged += OnReceiveData;
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
            //Unsub
            Global.StaticPropertyChanged -= OnReceiveData;
            //Recording warning
            if (timer1.Enabled)
            {
                MessageBox.Show("The recording is ON.\nStop it before quitting", "Warning");
                e.Cancel = true;
                return;
            }

            //Quitting with savings
            DialogResult dialogResult = MessageBox.Show("Do You Want To Save Your Data", "Save", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                SaveConfig();
                
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
        void SaveConfig() {
            MockDataStore store = new MockDataStore();
            store.SaveToProperties(config);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            config.track_bar = trackBar1.Value;
        }

        private void record_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                
                record_button.Text = "REC";
            }
            else
            {
                timer1.Start();
                record_textBox.Text = "0,0";
                record_button.Text = "●";
            }
        }

        private void decrease_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value < 1)
                return;

            trackBar1.Value -= 1;
            config.track_bar = trackBar1.Value;
        }

        private void increase_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value > trackBar1.Maximum-1)
                return;

            trackBar1.Value += 1;
            config.track_bar = trackBar1.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            record_textBox.Text = (Math.Round(Double.Parse(record_textBox.Text) + 0.1,1)).ToString();
        }

        #region Only numbers
        
        private void revs_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!keyPressCheck(sender, e))
                return;
        }

        bool keyPressCheck(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
                return true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
                return true;
            }
            return false;
        }

        private void t_gas_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!keyPressCheck(sender, e))
                return;
        }

        private void t_red_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!keyPressCheck(sender, e))
                return;
        }

        private void gas_time_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!keyPressCheck(sender, e))
                return;
        }

        private void petrol_time_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!keyPressCheck(sender, e))
                return;
        }

        private void g_press_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!keyPressCheck(sender, e))
                return;
        }

        private void map_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!keyPressCheck(sender, e))
                return;
        }

        private void l_on_m_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!keyPressCheck(sender, e))
                return;
        }
        #endregion
    }
}
