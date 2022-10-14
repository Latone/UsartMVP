using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Services
{
    /// <summary>
    /// Интерфейс класса MockDataStore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataStore<T>
    {
        Task<bool> SaveToProperties(T item);
        Task<T> GetFromProperties();
    }
}
