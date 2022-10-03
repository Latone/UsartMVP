using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Services
{
    public interface IDataStore<T>
    {
        Task<bool> SaveToProperties(T item);
        Task<T> GetFromProperties();
    }
}
