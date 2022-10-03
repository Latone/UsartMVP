
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
            this.port_Box = new System.Windows.Forms.TextBox();
            this.Config_status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Connect_button
            // 
            this.Connect_button.Location = new System.Drawing.Point(70, 377);
            this.Connect_button.Name = "Connect_button";
            this.Connect_button.Size = new System.Drawing.Size(93, 43);
            this.Connect_button.TabIndex = 0;
            this.Connect_button.Text = "Подключиться";
            this.Connect_button.UseVisualStyleBackColor = true;
            this.Connect_button.Click += new System.EventHandler(this.Connect_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(639, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Статус подключения:";
            // 
            // conn_status
            // 
            this.conn_status.AutoSize = true;
            this.conn_status.Location = new System.Drawing.Point(639, 422);
            this.conn_status.Name = "conn_status";
            this.conn_status.Size = new System.Drawing.Size(63, 13);
            this.conn_status.TabIndex = 2;
            this.conn_status.Text = "Отключено";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(68, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 45);
            this.button1.TabIndex = 3;
            this.button1.Text = "Выбор конфигурации";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_config_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(224, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 45);
            this.button2.TabIndex = 4;
            this.button2.Text = "Авто настройка";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_calibration_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(398, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 45);
            this.button3.TabIndex = 5;
            this.button3.Text = "Просмотр";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(580, 71);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 45);
            this.button4.TabIndex = 6;
            this.button4.Text = "Выбор языка";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(68, 180);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 36);
            this.button5.TabIndex = 7;
            this.button5.Text = "Загрузка данных";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(224, 179);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(90, 37);
            this.button6.TabIndex = 8;
            this.button6.Text = "Сохранение данных";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(398, 179);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(101, 37);
            this.button7.TabIndex = 9;
            this.button7.Text = "Новая прошивка";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(580, 179);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(96, 37);
            this.button8.TabIndex = 10;
            this.button8.Text = "Диаграммы";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(639, 346);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Порт подключения:";
            // 
            // port_Box
            // 
            this.port_Box.Location = new System.Drawing.Point(642, 362);
            this.port_Box.Name = "port_Box";
            this.port_Box.Size = new System.Drawing.Size(100, 20);
            this.port_Box.TabIndex = 12;
            // 
            // Config_status
            // 
            this.Config_status.AutoSize = true;
            this.Config_status.Location = new System.Drawing.Point(281, 278);
            this.Config_status.Name = "Config_status";
            this.Config_status.Size = new System.Drawing.Size(0, 13);
            this.Config_status.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Config_status);
            this.Controls.Add(this.port_Box);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.conn_status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Connect_button);
            this.Name = "Form1";
            this.Text = "Главное меню";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.TextBox port_Box;
        private System.Windows.Forms.Label Config_status;
    }
}

