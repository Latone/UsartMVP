
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
            this.Connect_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.conn_status = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Config_status = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.port_box = new System.Windows.Forms.ComboBox();
            this.update_ports = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Connect_button
            // 
            this.Connect_button.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Connect_button.Location = new System.Drawing.Point(36, 355);
            this.Connect_button.Name = "Connect_button";
            this.Connect_button.Size = new System.Drawing.Size(144, 43);
            this.Connect_button.TabIndex = 0;
            this.Connect_button.Text = "Подключиться";
            this.Connect_button.UseVisualStyleBackColor = true;
            this.Connect_button.Click += new System.EventHandler(this.Connect_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(508, 397);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Статус подключения:";
            // 
            // conn_status
            // 
            this.conn_status.AutoSize = true;
            this.conn_status.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.conn_status.Location = new System.Drawing.Point(508, 417);
            this.conn_status.Name = "conn_status";
            this.conn_status.Size = new System.Drawing.Size(92, 19);
            this.conn_status.TabIndex = 2;
            this.conn_status.Text = "Отключено";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(35, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 74);
            this.button1.TabIndex = 3;
            this.button1.Text = "Выбор конфигурации";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_config_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(237, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 74);
            this.button2.TabIndex = 4;
            this.button2.Text = "Авто настройка";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_calibration_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(430, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 74);
            this.button3.TabIndex = 5;
            this.button3.Text = "Просмотр";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.view_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Location = new System.Drawing.Point(633, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(129, 74);
            this.button4.TabIndex = 6;
            this.button4.Text = "Выбор языка";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button5.Enabled = false;
            this.button5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.Location = new System.Drawing.Point(35, 83);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(129, 74);
            this.button5.TabIndex = 7;
            this.button5.Text = "Загрузка данных";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button6.Enabled = false;
            this.button6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button6.Location = new System.Drawing.Point(237, 83);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(123, 74);
            this.button6.TabIndex = 8;
            this.button6.Text = "Сохранение данных";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button7.Enabled = false;
            this.button7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button7.Location = new System.Drawing.Point(430, 83);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(134, 74);
            this.button7.TabIndex = 9;
            this.button7.Text = "Новая прошивка";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button8.Location = new System.Drawing.Point(633, 83);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(129, 74);
            this.button8.TabIndex = 10;
            this.button8.Text = "Диаграммы";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(508, 356);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Порт подключения:";
            // 
            // Config_status
            // 
            this.Config_status.AutoSize = true;
            this.Config_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Config_status.Location = new System.Drawing.Point(284, 290);
            this.Config_status.Name = "Config_status";
            this.Config_status.Size = new System.Drawing.Size(0, 24);
            this.Config_status.TabIndex = 13;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button6, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.button8, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.button7, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(798, 160);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // port_box
            // 
            this.port_box.FormattingEnabled = true;
            this.port_box.Location = new System.Drawing.Point(674, 355);
            this.port_box.Name = "port_box";
            this.port_box.Size = new System.Drawing.Size(89, 21);
            this.port_box.TabIndex = 15;
            // 
            // update_ports
            // 
            this.update_ports.Location = new System.Drawing.Point(766, 355);
            this.update_ports.Name = "update_ports";
            this.update_ports.Size = new System.Drawing.Size(22, 21);
            this.update_ports.TabIndex = 16;
            this.update_ports.Text = "Update";
            this.update_ports.UseVisualStyleBackColor = true;
            this.update_ports.Click += new System.EventHandler(this.update_ports_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.update_ports);
            this.Controls.Add(this.port_box);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Config_status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.conn_status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Connect_button);
            this.Name = "Form1";
            this.Text = "Главное меню";
            this.Activated += new System.EventHandler(this.Form1_Activated);
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
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Config_status;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox port_box;
        private System.Windows.Forms.Button update_ports;
    }
}

