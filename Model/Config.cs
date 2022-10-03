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
        public string GAS_TIME { get; set; } = "";
        public string PETROL_TIME { get; set; } = "";
        public string G_PRES { get; set; } = "";
        public string MAP { get; set; } = "";
        public string TABLE_REVS { get; set; } = "";
        public string TABLE_REVS_Column { get; set; } = "0";
        public string TABLE_REVS_Row { get; set; } = "0";
        public bool inj_sens { get; set; } = true;
        public string l_on_mazda{ get; set; } = "";

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
    }
}
