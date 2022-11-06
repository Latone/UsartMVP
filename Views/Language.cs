using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Views
{
    public partial class Language : LocalizedForm
    {
        private ComponentResourceManager resources;
        public Language()
        {
            InitializeComponent();
        }
        private void ChangeLanguage(Control ctl, string lang)
        {
            resources.ApplyResources(ctl, ctl.Name, new CultureInfo(lang));
            foreach (Control c in ctl.Controls) ChangeLanguage(c, lang);
            //
        }
        private void rus_b_Click(object sender, EventArgs e)
        {
            StaticLangManager.GlobalUICulture = new CultureInfo("ru-RU");
            //resources = new ComponentResourceManager(typeof(Form1));
            //ChangeLanguage(this, "ru-RU");
        }

        private void eng_b_Click(object sender, EventArgs e)
        {
             StaticLangManager.GlobalUICulture =  new CultureInfo("en-US");
             //resources = new ComponentResourceManager(typeof(Form1));
             //ChangeLanguage(this, "en-US");
        }
    }
}
