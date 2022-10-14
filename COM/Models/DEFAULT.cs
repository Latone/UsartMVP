using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.COM.Models
{
    /// <summary>
    /// 
    /// Подтип Model
    /// (Базовый, на получение фреймов *D* и *D#)
    /// 
    /// Тип отправляемых/получаемых пакетов
    /// 
    /// </summary>
    public class DEFAULT: IModel<DEFAULT.items>
    {
        public Result<items> result { get; set; }

        //Type T:
        public class items
        {
            public byte[] data { get; set; }
        }
    }
}
