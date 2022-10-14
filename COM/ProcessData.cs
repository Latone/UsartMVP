using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.COM.Models;

namespace WindowsFormsApp1.COM
{
    /// <summary>
    /// 
    /// Класс обработки получаемых пакетов
    /// T - тип обрабатываемых пакетов. См. COM.Models
    /// 
    /// </summary>
    public class ProcessData<T>
    {
        static private int counter_bytes = 0;                               //Кол-во байтов на время записи
        static private byte[] BufferByte = new byte[2048];                  //Общий буфер, в который записываются байты
        static private string str1 = "", str2 = "", str3 = "", str4 = "";   //>
        static private int resultat, resultat1 = 0;                         //>Промежуточные переменные
        static private SerialPort MyserialPort;                             //Подключенный порт

        //GetAsyncXN - Обрабатывает типы фреймов XNZZZ
        static T GetAsyncXN()
        {
            var model = new XN();
            Result<XN.items> res = model.result;
            res.items = new List<XN.items>();
            res.items.Add(new XN.items());
            res.items[0].data = new byte[45];
            
            try
            {
                //Retreive answer
                Thread.Sleep(350); //Will be changed with band speed
                counter_bytes = MyserialPort.BytesToRead;
                string answer = "";
                for (int i = 0; i < counter_bytes; i++)
                {
                    BufferByte[i] = (byte)MyserialPort.ReadByte();   // Читаем один Байт из входного 
                }
                for (int j = 0; j < counter_bytes; j++)
                {
                    if (j + 1 < counter_bytes && ((char)BufferByte[j] == 'X') && (BufferByte[j + 1] == 'N'))
                    {
                        answer += (char)BufferByte[j + 3];
                        answer += (char)BufferByte[j + 4];
                        answer += (char)BufferByte[j + 5];
                        answer += (char)BufferByte[j + 6];
                        answer += (char)BufferByte[j + 7];
                        answer += (char)BufferByte[j + 8];

                        break;                               //Как только нашли вхождение то стоп чтение в этом цикле
                    }
                }
                if (answer != Response_Type.OK.ToString())
                    return (T)new object();

                ///Посылаем подтип XNZZ1, XNZZ2 .. на обработку контроллером
                Thread.Sleep(350); //Изменится с иной скоростью передачи по COM
                counter_bytes = MyserialPort.BytesToRead;

                for (int i = 0; i < counter_bytes; i++)
                {
                    BufferByte[i] = (byte)MyserialPort.ReadByte();   // Читаем один Байт из входного 
                }
                //*****************************************************************
               
                //Case XNZZ1
                for (int j = 0; j < counter_bytes; j++)
                {
                    if (j + 2 < counter_bytes && ((char)BufferByte[j] == '*') && (BufferByte[j + 1] == 'D') && (BufferByte[j + 2] == 'R'))
                    {
                        res.items[0].data[0] = BufferByte[j + 3];
                        res.items[0].data[1] = BufferByte[j + 4];
                        res.items[0].data[2] = BufferByte[j + 5];
                        res.items[0].data[3] = BufferByte[j + 6];
                        res.items[0].data[4] = BufferByte[j + 7];
                        res.items[0].data[5] = BufferByte[j + 8];

                        break;                               //Как только нашли вхождение то стоп чтение в этом цикле
                    }
                }
                //Обработка байтов, перепись данных в конфигурацию
                #region XNZZZ
                //Фрейм *DR
                resultat1 = res.items[0].data[1];
                resultat1 = resultat1 << 8;
                resultat1 = (resultat1 | res.items[0].data[0]);

                Global.config.TABLE_REVS_Column = resultat1.ToString();

                resultat1 = res.items[0].data[3];
                resultat1 = resultat1 << 8;
                resultat1 = (resultat1 | res.items[0].data[2]);

                Global.config.TABLE_REVS_Row = resultat1.ToString();

                resultat1 = res.items[0].data[5];
                resultat1 = resultat1 << 8;
                resultat1 = (resultat1 | res.items[0].data[4]);

                Global.config.TABLE_REVS = resultat1.ToString();
                #endregion
            }
            catch (Exception ex)
            {
                //Обработка на внезапное отключение COM-порта
                Global.config.connection_error = true;
                Global.COMdisconnect();
                return (T)(object)model;
            }
            //Сохранение конфигурации
            Global.updateConfig();
            model.result = res;

            return (T)(object)model;
        }
            static T GetAsyncDef()
        {
            var model = new DEFAULT();
            Result<DEFAULT.items> res = model.result;
            res.items = new List<DEFAULT.items>();
            res.items.Add(new DEFAULT.items());
            res.items[0].data = new byte[45];

            
            try
            {
                Thread.Sleep(350); //Изменится с иной скоростью передачи по COM
                counter_bytes = MyserialPort.BytesToRead;

                for (int i = 0; i < counter_bytes; i++)
                {
                    BufferByte[i] = (byte)MyserialPort.ReadByte();   // Читаем один Байт из входного 
                }
                //*****************************************************************
                for (int j = 0; j < counter_bytes; j++)
                {
                    if (j + 2 <= counter_bytes - 1 && ((char)BufferByte[j] == '*') && (BufferByte[j + 1] == 'D') && (BufferByte[j + 2] == '*'))
                    {
                        j += 3;
                        res.items[0].data[0] = BufferByte[j++];
                        res.items[0].data[1] = BufferByte[j++];
                        res.items[0].data[2] = BufferByte[j++];
                        res.items[0].data[3] = BufferByte[j++];
                        res.items[0].data[4] = BufferByte[j++];
                        res.items[0].data[5] = BufferByte[j++];
                        res.items[0].data[6] = BufferByte[j++];
                        res.items[0].data[7] = BufferByte[j++];
                        res.items[0].data[8] = BufferByte[j++];
                        res.items[0].data[9] = BufferByte[j++];
                        if (res.items[0].data[0] != 0 && res.items[0].data[10] != 0)
                            break;
                    }
                    if (j + 2 < counter_bytes && ((char)BufferByte[j] == '*') && (BufferByte[j + 1] == 'D') && (BufferByte[j + 2] == '#'))
                    {
                        res.items[0].data[10] = BufferByte[j + 3];
                        res.items[0].data[11] = BufferByte[j + 4];
                        res.items[0].data[12] = BufferByte[j + 5];
                        res.items[0].data[13] = BufferByte[j + 6];
                        res.items[0].data[14] = BufferByte[j + 7];
                        res.items[0].data[15] = BufferByte[j + 8];
                        res.items[0].data[16] = BufferByte[j + 9];
                        res.items[0].data[17] = BufferByte[j + 10];
                        res.items[0].data[18] = BufferByte[j + 11];
                        res.items[0].data[19] = BufferByte[j + 12];
                        res.items[0].data[20] = BufferByte[j + 13];
                        res.items[0].data[21] = BufferByte[j + 14];
                        if (res.items[0].data[0] != 0 && res.items[0].data[10] != 0)
                            break;                               //Как только нашли вхождение то стоп чтение в этом цикле
                    }
                }
                //Обработка байтов, перепись данных в конфигурацию
                        #region DEFAULT
                        //Frame *D*
                        resultat = res.items[0].data[1];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat = resultat << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat = (resultat | res.items[0].data[0]);               // И склеиваем с младшим.

                        Global.config.REVS = resultat.ToString();

                        resultat1 = res.items[0].data[3];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat1 = resultat1 << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat1 = (resultat1 | res.items[0].data[2]);               // И склеиваем с младшим.

                        resultat = (resultat * 1000) / 1000 - 990;



                        str1 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                        Global.config.time_1 = str1;

                        resultat1 = res.items[0].data[5];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat1 = resultat1 << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat1 = (resultat1 | res.items[0].data[4]);               // И склеиваем с младшим.

                        str2 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                        Global.config.time_2 = str2;

                        resultat1 = res.items[0].data[7];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat1 = resultat1 << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat1 = (resultat1 | res.items[0].data[6]);               // И склеиваем с младшим.

                        str3 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                        Global.config.time_3 = str3;

                        resultat1 = res.items[0].data[9];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat1 = resultat1 << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat1 = (resultat1 | res.items[0].data[8]);               // И склеиваем с младшим.

                        str4 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                        Global.config.time_4 = str4;

                        //Frame *D#

                        resultat1 = res.items[0].data[11];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat1 = resultat1 << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat1 = (resultat1 | res.items[0].data[10]);               // И склеиваем с младшим.

                        //str2 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                        Global.config.T_RED = resultat1.ToString();

                        resultat1 = res.items[0].data[13];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat1 = resultat1 << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat1 = (resultat1 | res.items[0].data[12]);               // И склеиваем с младшим.

                        //str3 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                        Global.config.T_GAS = resultat1.ToString();

                        resultat1 = res.items[0].data[15];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat1 = resultat1 << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat1 = (resultat1 | res.items[0].data[14]);               // И склеиваем с младшим.

                        //str4 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                        Global.config.T_AIR = resultat1.ToString();

                        resultat1 = res.items[0].data[17];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat1 = resultat1 << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat1 = (resultat1 | res.items[0].data[16]);               // И склеиваем с младшим.

                        str3 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                        Global.config.G_PRES = str3;

                        resultat1 = res.items[0].data[19];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat1 = resultat1 << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat1 = (resultat1 | res.items[0].data[18]);               // И склеиваем с младшим.

                        str4 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                        Global.config.MAP = str4;

                        resultat1 = res.items[0].data[21];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                        resultat1 = resultat1 << 8;                    // res.items[0].data2  это старший байт и смещаем его влево.
                        resultat1 = (resultat1 | res.items[0].data[20]);               // И склеиваем с младшим.

                        Global.config.test_time = resultat1.ToString();
                    #endregion

                  
                MyserialPort.DiscardInBuffer();
                //MyserialPort.DiscardOutBuffer();
            }
            catch (Exception ex)
            {
                //Обработка на внезапное отключение COM-порта
                Global.config.connection_error = true;
                Global.COMdisconnect();
                return (T)(object)model;
            }

            //Сохранение конфигурации
            Global.updateConfig();
            model.result = res;
            return (T)(object)model;
        }

        //Стартовая точка
        ///Не обязательно возвращать модель с обработанными данными T
        public static T Run(SerialPort sp)
        {
            MyserialPort = sp;
            
            var resultModel = (dynamic)null;
            try
            {
                //Выбор типа пакетов (уже предложенных/выбранных с GUI)
                switch (typeof(T).Name) {
                    case "XN":
                        resultModel = GetAsyncXN();
                        break;
                    case "DEFAULT":
                        resultModel = GetAsyncDef();
                        break;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return resultModel;
            }
            return resultModel;
        }
    }
}
