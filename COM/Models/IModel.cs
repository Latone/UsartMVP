using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.COM.Models
{
    /// <summary>
    /// 
    /// Интерфейс IModel
    /// 
    /// Описывает типы отправляемых/получаемых пакетов
    /// 
    /// </summary>
    public interface IModel<T>
    {
       Result<T> result { get; set; }
    }
    //Общие типы ошибок
    public enum Response_Type
    {
        OK,
        ERROR,
        BAD_R, //Bad request
        BUSY    
    }
    public struct Result<T> {
        public Response_Type response { get; set; }
        public List<T> items { get; set; } 
    }
}
