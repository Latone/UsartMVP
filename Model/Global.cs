using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    /// <summary>
    /// Класс отвечающий за своевременное обновление информации в Forms
    /// </summary>
    public static class Global
    {
        static void NotifyStaticPropertyChanged([CallerMemberName] string propertyName = "")
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
        public static event PropertyChangedEventHandler StaticPropertyChanged; //Можно подписываться на него

        //Event на обновление конфига (вызывается из любой точки программы)
        public static void updateConfig()
        {
            NotifyStaticPropertyChanged("Config Update");
        }

        //Event на разрыв соединения, ловится try-catch при обработке пакетов (вызывается из любой точки программы)
        public static void COMdisconnect()
        {
            NotifyStaticPropertyChanged("COM Disconnected");
        }

        //Объявление глобального конфига
        private static Config _config = null;

        public static Config config
        {
            get { return _config; }
            set { _config = value; }
        }

    }
}
