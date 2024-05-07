using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CitiesInfo
{
    public class XMLmethods
    {
        public static void ListToXMLUsingSerializer<T>(List<T> items, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(stream, items);
            }

            string fullPath = Path.GetFullPath(fileName);

            Console.WriteLine($"XML файл створено за назвою {fileName}. Шлях до файлу - {fullPath}");
        }

        public static List<T> XMLToListUsingSerializer<T>(string fileName)
        {
            List<T> items;

            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                items = (List<T>)serializer.Deserialize(stream);
            }

            return items;
        }
    }
}
