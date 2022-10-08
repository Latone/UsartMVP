using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    public static class Global
    {
        static void NotifyStaticPropertyChanged([CallerMemberName] string propertyName = "")
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
        public static event PropertyChangedEventHandler StaticPropertyChanged; //Можно подписываться на него

        public static void updateConfig()
        {
            NotifyStaticPropertyChanged("Config Update");
        }
        public static void COMdisconnect()
        {
            NotifyStaticPropertyChanged("COM Disconnected");
        }

        private static Config _config = null;

        public static Config config
        {
            get { return _config; }
            set { _config = value; }
        }

    }
}
