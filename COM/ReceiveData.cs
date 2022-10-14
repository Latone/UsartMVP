using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using WindowsFormsApp1.Model;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApp1.COM.Models;

namespace WindowsFormsApp1.COM
{
    /// <summary>
    /// Класс на получение данных 
    /// </summary>
    public class ReceiveData : EventArgs
    {
        public ReceiveData(Config cnfg)
        {
            try
            {
                Global.config = cnfg;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Global.config.connection_error = true;
                Global.COMdisconnect();
                return;
            }
        }

        //Подписанный event на получение данных
        public static void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                EvaluateData(sp);
            }
            catch (Exception ex)
            {
                //При отключении, иных ошибках
                Global.config.connection_error = true;
                Global.COMdisconnect();
                return;
            }
            }
        private static void EvaluateData(SerialPort MyserialPort)
        {
            //Пока обрабатывается 1 тип пакетов без выбора в GUI
            var model = new DEFAULT();


            Console.WriteLine("Производится вызов по запросу..");

            model = ProcessData<DEFAULT>.Run(MyserialPort);

            if (model == null)
            {
                throw new Exception("Запрос не обработался за определённое время из-за нагрузки");
            }

        }

    }
}
