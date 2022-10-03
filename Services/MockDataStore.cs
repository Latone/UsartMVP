using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Services
{
    public class MockDataStore : IDataStore<Config>
    {
        public Task<bool> SaveToProperties(Config item)
        {
            try {
                var convertedData = JsonSerializer.Serialize(item);
                Properties.Settings.Default["ConfigProps"] = convertedData;
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
                var json = Properties.Settings.Default["ConfigProps"];
                if (json == "")
                    return Task.FromResult(new Config());

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
