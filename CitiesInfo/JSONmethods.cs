using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CitiesInfo
{
    public class JSONmethods 
    {
        public static void ListToJSONusingSerializer<T>(List<T> items, string filePath)
        {
            string json = JsonConvert.SerializeObject(items);
            File.WriteAllText(filePath, json);
        }
        public static List<T> JSONtoListUsingSerializer<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не знайдено.");
                return null;
            }

            string json = File.ReadAllText(filePath);

            List<T> items = JsonConvert.DeserializeObject<List<T>>(json);

            return items;
        }
    }
}
