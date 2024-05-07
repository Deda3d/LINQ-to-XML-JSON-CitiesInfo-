using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CitiesInfo
{
    public class Resident
    {
        public int ResidentId { get; set; }
        public string Name { get; set; }

        public Resident() { }
        public Resident(string Name)
        {
            this.ResidentId = GlobalIndexes.last_resident_index++;
            this.Name = Name;
        }

        public static void WriteToXmlFile(List<Resident> residents, string filePath)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };

            CultureInfo cultureInfo = new CultureInfo("en-US");

            using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("ArrayOfResident");

                foreach (var resident in residents)
                {
                    writer.WriteStartElement("Resident");

                    writer.WriteElementString("ResidentId", resident.ResidentId.ToString());
                    writer.WriteElementString("Name", resident.Name);

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public static void CreateNewResidentsByConsole()
        {
            List<Resident> residents = new List<Resident>();
            GlobalIndexes.last_resident_index = 0;
            bool end = false;
            while (!end)
            {
                Console.Clear();
                Console.WriteLine($"Додано {residents.Count} мешканців. Впишіть end щоб завершити.");
                Console.WriteLine();
                Console.Write("Введіть ім'я мешканця або end, щоб завершити: ");
                string Name = Console.ReadLine();
                if (Name != "end")
                {
                    Resident resident = new Resident(Name);
                    residents.Add(resident);
                }
                else end = true;
            }
            Resident.WriteToXmlFile(residents, "residents.xml");
            Console.WriteLine($"XML файл було успішно створено");
            Console.ReadLine();
            Console.Clear();
        }
        public static void AddNewResidentsByConsole()
        {
            List<Resident> existResidents = XMLmethods.XMLToListUsingSerializer<Resident>("residents.xml");
            List<Resident> residentsToAdd = new List<Resident>();
            GlobalIndexes.last_resident_index = existResidents.Count;
            bool end = false;
            while (!end)
            {
                Console.Clear();
                Console.WriteLine($"Додано {residentsToAdd.Count} мешканців. Впишіть end щоб завершити.");
                Console.WriteLine();
                Console.Write("Введіть ім'я мешканця або end, щоб завершити: ");
                string Name = Console.ReadLine();
                if (Name != "end")
                {
                    Resident resident = new Resident(Name);
                    residentsToAdd.Add(resident);
                }
                else end = true;
            }
            existResidents.AddRange(residentsToAdd);
            Resident.WriteToXmlFile(existResidents, "residents.xml");
            Console.WriteLine($"XML файл було успішно створено");
            Console.ReadLine();
            Console.Clear();
        }

        public static void PrintAllResidentsUsingSerialization()
        {
            List<Resident> residents = XMLmethods.XMLToListUsingSerializer<Resident>("residents.xml");
            foreach (var res in residents)
            {
                Console.WriteLine($"{res.ResidentId}. {res.Name}");
            }
        }
        public static void PrintAllResidentsUsingDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load("residents.xml");

                XmlNodeList residentNodes = xmlDoc.SelectNodes("//Resident");

                foreach (XmlNode residentNode in residentNodes)
                {
                    XmlNode residentIdNode = residentNode.SelectSingleNode("ResidentId");
                    string residentId = residentIdNode.InnerText;

                    XmlNode nameNode = residentNode.SelectSingleNode("Name");
                    string name = nameNode.InnerText;

                    Console.WriteLine($"{residentId}. {name}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading XML file: {e.Message}");
            }
        }
    }
}
