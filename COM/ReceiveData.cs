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
        private static byte[] data;
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
                //throw new Exception(ex.Message);
                Global.config.connection_error = true;
                Global.COMdisconnect();
                return;
            }
            }
        private static void EvaluateData(SerialPort MyserialPort)
        {
            // MyserialPort.DiscardInBuffer();
            data = new byte[22];
            int counter_bytes = 0;
            byte[] BufferByte= new byte[2048];
            string str="",str1="",str2="",str3="",str4="";
            bool flag_act = false;
            int resultat, resultat1 = 0;

            try
            {
                Thread.Sleep(350);
                counter_bytes = MyserialPort.BytesToRead;
            
                for (int i = 0; i < counter_bytes; i++)
                {
                    
                        BufferByte[i] = (byte)MyserialPort.ReadByte();   // Читаем один Байт из входного буфера
                 
                }
                //*****************************************************************
                for (int j = 0; j < counter_bytes; j++)
                {
                    if (j + 2 <= counter_bytes - 1 && ((char)BufferByte[j] == '*') && (BufferByte[j + 1] == 'D') && (BufferByte[j + 2] == '*'))
                    {
                    j += 3;
                        data[0] = BufferByte[j++];
                        data[1] = BufferByte[j++];
                        data[2] = BufferByte[j++];
                        data[3] = BufferByte[j++];
                        data[4] = BufferByte[j++];
                        data[5] = BufferByte[j++];
                        data[6] = BufferByte[j++];
                        data[7] = BufferByte[j++];
                        data[8] = BufferByte[j++];
                        data[9] = BufferByte[j++];
                    if (data[0] != 0 && data[10] != 0)
                        break;
                }
                if (j + 2 < counter_bytes && ((char)BufferByte[j] == '*') && (BufferByte[j + 1] == 'D') && (BufferByte[j + 2] == '#'))
                {
                    data[10] = BufferByte[j + 3];
                    data[11] = BufferByte[j + 4];
                    data[12] = BufferByte[j + 5];
                    data[13] = BufferByte[j + 6];
                    data[14] = BufferByte[j + 7];
                    data[15] = BufferByte[j + 8];
                    data[16] = BufferByte[j + 9];
                    data[17] = BufferByte[j + 10];
                    data[18] = BufferByte[j + 11];
                    data[19] = BufferByte[j + 12];
                    data[20] = BufferByte[j + 13];
                    data[21] = BufferByte[j + 14];
                    if(data[0] != 0 && data[10]!=0)
                        break;                               //Как только нашли вхождение то стоп чтение в этом цикле
                }
            }
                //if (data[0] == 0 || data[10] == 0)
                    //Thread.Sleep(500);
                //*****************************************************************
                resultat = data[1];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                resultat = resultat << 8;                    // data2  это старший байт и смещаем его влево.
                resultat = (resultat | data[0]);               // И склеиваем с младшим.

                Global.config.test_pressure = resultat.ToString();

                resultat1 = data[3];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
                resultat1 = (resultat1 | data[2]);               // И склеиваем с младшим.

                resultat = (resultat * 1000) / 1000 - 990;
                
                

                str1 = Convert.ToString(resultat1 /100 + "." + (resultat1 % 100));
                Global.config.time_1 = str1;

                resultat1 = data[5];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
                resultat1 = (resultat1 | data[4]);               // И склеиваем с младшим.

                str2 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                Global.config.time_2 = str2;

                resultat1 = data[7];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
                resultat1 = (resultat1 | data[6]);               // И склеиваем с младшим.

                str3 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                Global.config.time_3 = str3;

                resultat1 = data[9];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
                resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
                resultat1 = (resultat1 | data[8]);               // И склеиваем с младшим.

                str4 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
                Global.config.time_4 = str4;

            //Second frame

            resultat1 = data[11];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
            resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
            resultat1 = (resultat1 | data[10]);               // И склеиваем с младшим.

            //str2 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
            Global.config.T_RED = resultat1.ToString();

            resultat1 = data[13];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
            resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
            resultat1 = (resultat1 | data[12]);               // И склеиваем с младшим.

            //str3 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
            Global.config.T_GAS = resultat1.ToString();

            resultat1 = data[15];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
            resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
            resultat1 = (resultat1 | data[14]);               // И склеиваем с младшим.

            //str4 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
            Global.config.T_AIR = resultat1.ToString();

            resultat1 = data[17];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
            resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
            resultat1 = (resultat1 | data[16]);               // И склеиваем с младшим.

            str3 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
            Global.config.G_PRES = str3;

            resultat1 = data[19];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
            resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
            resultat1 = (resultat1 | data[18]);               // И склеиваем с младшим.

            str4 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
            Global.config.MAP = str4;

            resultat1 = data[21];                            // Здесь склейка поступающих данных из 2*8 -> 16 бит.
            resultat1 = resultat1 << 8;                    // data2  это старший байт и смещаем его влево.
            resultat1 = (resultat1 | data[20]);               // И склеиваем с младшим.

            //str4 = Convert.ToString(resultat1 / 100 + "." + (resultat1 % 100));
            Global.config.test_time = resultat1.ToString();

            str = "";
            MyserialPort.DiscardInBuffer();

                //MyserialPort.DiscardOutBuffer();
            }
            catch(Exception ex)
            {
                //throw new Exception(ex.Message);
                Global.config.connection_error = true;
                Global.COMdisconnect();
                return;
            }
            Global.updateConfig();
        }

    }
}
