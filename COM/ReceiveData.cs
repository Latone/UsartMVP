using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using WindowsFormsApp1.Model;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1.COM
{
    public class ReceiveData : EventArgs
    {
        private static byte data1, data2, data3, data4, data5, data6, data7, data8, data9, data10;

        public ReceiveData(Config cnfg)
        {
            Global.config = cnfg;
        }
        public static void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            EvaluateData(sp);
        }
        private static void EvaluateData(SerialPort MyserialPort)
        {
           // MyserialPort.DiscardInBuffer();
            int counter_bytes = 0;
            byte[] BufferByte= new byte[2048];
            string str="",str1="",str2="",str3="",str4="";
            bool flag_act = false;
            int resultat, resultat1 = 0;

            try
            {
                counter_bytes = MyserialPort.BytesToRead;
            }
            catch
            {
                Global.COMdisconnect();
                return;
            }
            

                for (int i = 0; i <= 20; i++)
                {
                    try
                    {
                        BufferByte[i] = (byte)MyserialPort.ReadByte();   // Читаем один Байт из входного буфера
                    }
                    catch (Exception ex)
                    {
                    Global.COMdisconnect();
                    return;
                }
                }
                //*****************************************************************
                for (int j = 0; j <= (counter_bytes - 1); j++)
                {
                    if (j + 2 <= counter_bytes - 1 && ((char)BufferByte[j] == '*') && (BufferByte[j + 1] == 'D') && (BufferByte[j + 2] == '*'))
                    {
                        data1 = BufferByte[j + 3];
                        data2 = BufferByte[j + 4];
                        data3 = BufferByte[j + 5];
                        data4 = BufferByte[j + 6];
                        data5 = BufferByte[j + 7];
                        data6 = BufferByte[j + 8];
                        data7 = BufferByte[j + 9];
                        data8 = BufferByte[j + 10];
                        data9 = BufferByte[j + 11];
                        data10 = BufferByte[j + 12];

                        break;                               //Как только нашли вхождение то стоп чтение в этом цикле
                    }
                }
                //*****************************************************************
                resultat = data2;                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                resultat = resultat << 8;                    // data2  это старший байт и смещаем его влево.
                resultat = (resultat | data1);               // И склеиваем с младшим.

                resultat1 = data4;                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
                resultat1 = (resultat1 | data3);               // И склеиваем с младшим.

                resultat = (resultat * 1000) / 1000 - 990;
                
                

                str1 = Convert.ToString(resultat1 /100 + "." + (resultat1 % 100));
                Global.config.REVS = str1;

                resultat1 = data6;                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
                resultat1 = (resultat1 | data5);               // И склеиваем с младшим.

                str2 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                Global.config.T_GAS = str2;

                resultat1 = data8;                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
                resultat1 = (resultat1 | data7);               // И склеиваем с младшим.

                str3 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                Global.config.T_RED = str3;

                resultat1 = data10;                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
                resultat1 = (resultat1 | data9);               // И склеиваем с младшим.

                str4 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                Global.config.GAS_TIME = str4;

                str = "";
                Global.updateConfig();

            MyserialPort.DiscardInBuffer();

            //MyserialPort.DiscardOutBuffer();
        }

    }
}
