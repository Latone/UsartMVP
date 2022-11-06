
namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Connect_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.conn_status = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ch_lang = new System.Windows.Forms.Button();
            this.load_butt = new System.Windows.Forms.Button();
            this.save_butt = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Config_status_1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.port_box = new System.Windows.Forms.ComboBox();
            this.update_ports = new System.Windows.Forms.Button();
            this.Config_status_2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Connect_button
            // 
            resources.ApplyResources(this.Connect_button, "Connect_button");
            this.Connect_button.Name = "Connect_button";
            this.Connect_button.UseVisualStyleBackColor = true;
            this.Connect_button.Click += new System.EventHandler(this.Connect_button_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // conn_status
            // 
            resources.ApplyResources(this.conn_status, "conn_status");
            this.conn_status.Name = "conn_status";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_config_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_calibration_Click);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.view_Click);
            // 
            // ch_lang
            // 
            resources.ApplyResources(this.ch_lang, "ch_lang");
            this.ch_lang.Name = "ch_lang";
            this.ch_lang.UseVisualStyleBackColor = true;
            this.ch_lang.Click += new System.EventHandler(this.ch_lang_Click);
            // 
            // load_butt
            // 
            resources.ApplyResources(this.load_butt, "load_butt");
            this.load_butt.Name = "load_butt";
            this.load_butt.UseVisualStyleBackColor = true;
            this.load_butt.Click += new System.EventHandler(this.load_butt_Click);
            // 
            // save_butt
            // 
            resources.ApplyResources(this.save_butt, "save_butt");
            this.save_butt.Name = "save_butt";
            this.save_butt.UseVisualStyleBackColor = true;
            this.save_butt.Click += new System.EventHandler(this.save_butt_Click);
            // 
            // button7
            // 
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            resources.ApplyResources(this.button8, "button8");
            this.button8.Name = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // Config_status_1
            // 
            resources.ApplyResources(this.Config_status_1, "Config_status_1");
            this.Config_status_1.Name = "Config_status_1";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.load_butt, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.save_butt, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ch_lang, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.button8, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.button7, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // port_box
            // 
            resources.ApplyResources(this.port_box, "port_box");
            this.port_box.FormattingEnabled = true;
            this.port_box.Name = "port_box";
            // 
            // update_ports
            // 
            resources.ApplyResources(this.update_ports, "update_ports");
            this.update_ports.Name = "update_ports";
            this.update_ports.UseVisualStyleBackColor = true;
            this.update_ports.Click += new System.EventHandler(this.update_ports_Click);
            // 
            // Config_status_2
            // 
            resources.ApplyResources(this.Config_status_2, "Config_status_2");
            this.Config_status_2.Name = "Config_status_2";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.Config_status_2);
            this.Controls.Add(this.update_ports);
            this.Controls.Add(this.port_box);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Config_status_1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.conn_status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Connect_button);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Form1";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connect_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label conn_status;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button ch_lang;
        private System.Windows.Forms.Button load_butt;
        private System.Windows.Forms.Button save_butt;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Config_status_1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox port_box;
        private System.Windows.Forms.Button update_ports;
        private System.Windows.Forms.Label Config_status_2;
    }
}

