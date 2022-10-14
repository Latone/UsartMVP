using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Services
{
    /// <summary>
    /// Класс, предназначенный для сохранения и извлечения параметров конфигурации в Properties.Settings
    /// </summary>
    public class MockDataStore : IDataStore<Config>
    {
        public Task<bool> SaveToProperties(Config item)
        {
            try {
                //Сереализация в Json 
                var convertedData = JsonSerializer.Serialize(item);
                //Сохранение строки Json (string) в ячейку ConfigProps
                Properties.Settings.Default["ConfigProps"] = convertedData;
                //Сохранение изменённых данных
                Properties.Settings.Default.Save();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return Task.FromResult(true);
        }

        public Task<Config> GetFromProperties()
        {
            var item = new Config();
            try
            {
                //Взятие созранённой (или нет) стркои из ячейки ConfigProps (тип Json)
                var json = Properties.Settings.Default["ConfigProps"];
                if (json == "")
                    return Task.FromResult(new Config());

                //Десериализация и сохранение в item (тип Config)
                item = JsonSerializer.Deserialize<Config>(JsonDocument.Parse(json.ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Task.FromResult(item);
        }
    }
}
