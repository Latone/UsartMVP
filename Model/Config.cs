using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    public enum radio_button
    {
        EXTRA_INJ = 0,
        CUT_OFF = 1,
        DIAGNOSTICS = 2
    }
    public class Config
    {
        public radio_button rb { get; set; }
        public string REVS { get; set; } = "";
        public string T_GAS { get; set; } = "";
        public string T_RED { get; set; } = "";

        public string T_AIR { get; set; } = "0";
        public string GAS_TIME { get; set; } = "";
        public string PETROL_TIME { get; set; } = "";
        public string G_PRES { get; set; } = "";
        public string MAP { get; set; } = "";
        public string TABLE_REVS { get; set; } = "";
        public string TABLE_REVS_Column { get; set; } = "0";
        public string TABLE_REVS_Row { get; set; } = "0";
        public bool inj_sens { get; set; } = true;
        public string l_on_mazda{ get; set; } = "";

        public string time_1 { get; set; } = "0";
        public string time_2 { get; set; } = "0";
        public string time_3 { get; set; } = "0";
        public string time_4 { get; set; } = "0";

        public string test_time { get; set; } = "0";

        public bool connection_error { get; set; } = false;
        public string test_pressure { get; set; } = "0";

        public int track_bar { get; set; } = 0;
        public string toString() {
            return "Revs: " + REVS + "\n" +
                "t_gas: " + T_GAS + "\n" +
                "t_red: " + T_RED + "\n" +
                "gas_time: " + GAS_TIME + "\n" +
                "petrol_time: " + PETROL_TIME + "\n" +
                "g_pres: " + G_PRES + "\n" +
                "map: " + MAP + "\n" +
                "table_revs: " + TABLE_REVS + "\n" +
                "inj_sens: " + inj_sens + "\n" +
                "l_on_mazda: " + l_on_mazda;
        }
        public string getBasicInfo1()
        {
            return "Pressure: " + test_pressure + "\n\n"+
                "time_span1: " + time_1 + "\n" +
                "time_span2: " + time_2 + "\n" +
                "time_span3: " + time_3 + "\n" +
                "time_span4: " + time_4;
        }
        public string getBasicInfo2()
        {
            return "T_RED " + T_RED + "\n" +
                "T_GAS: " + T_GAS + "\n" +
                "T_AIR: " + T_AIR + "\n" +
                "G_PRES: " + G_PRES + "\n" +
                "MAP: " + MAP+ "\n" +
                "test_count: " + test_time;
        }
    }
}
