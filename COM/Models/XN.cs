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
    /// 
    /// Тип отправляемых/получаемых пакетов
    /// 
    /// </summary>
    public class XN : IModel<XN.items>
    {
        public Result<items> result { get; set; }

        //Типы пакетов на обработку
        public enum Requset_SubType
        {
            XNZZ1,
            XNZZ2,
            XNZZ3
        }
        //Type T:
        public class items
        {
            public Requset_SubType subtype { get; set; }
            public byte[] data { get; set; }
        }
    }
}
