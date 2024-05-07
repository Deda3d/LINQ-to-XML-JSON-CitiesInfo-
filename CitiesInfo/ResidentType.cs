using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CitiesInfo
{
    public class ResidentType
    {
        public int ResidentTypeId { get; set; }
        public string Name { get; set; }
        public List<Resident> Residents { get; set; }
        public ResidentType(string TypeName)
        {
            this.ResidentTypeId = GlobalIndexes.last_residentType_index++;
            this.Name = TypeName;
            this.Residents = new List<Resident>();
        }
        public ResidentType() { }

        public static void AddResidentsToType(string typeName, List<ResidentType> allTypes, List<Resident> allResidents, string[] nameResidentsToAdd)
        {
            ResidentType type = allTypes.FirstOrDefault(t => t.Name == typeName);
            if (type != null)
            {
                foreach (string resident in nameResidentsToAdd)
                {
                    if (allResidents.Any(c => c.Name == resident)) type.AddResident(allResidents.FirstOrDefault(c => c.Name == resident));
                    else Console.WriteLine($"Виникла помилка. Людини з ім'ям {resident} не існує");
                }
            }
            else Console.WriteLine($"Тип мешканця '{typeName}' не знайдено.");
        }
        private void AddListResidents(List<Resident> residents)
        {
            this.Residents = residents;
        }
        public void AddResident(Resident resident)
        {
            this.Residents.Add(resident);
        }

        public static void WriteToXmlFile(List<ResidentType> types, string filePath)
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
                writer.WriteStartElement("ArrayOfResidentType");

                foreach (var type in types)
                {
                    writer.WriteStartElement("ResidentType");

                    writer.WriteElementString("ResidentTypeId", type.ResidentTypeId.ToString());
                    writer.WriteElementString("Name", type.Name);

                    writer.WriteStartElement("Residents");
                    foreach (var resident in type.Residents)
                    {
                        writer.WriteStartElement("Resident");
                        writer.WriteElementString("ResidentId", resident.ResidentId.ToString());
                        writer.WriteElementString("Name", resident.Name);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public static void CreateNewResidentTypesByConsole()
        {
            List<Resident> existResidents = XMLmethods.XMLToListUsingSerializer<Resident>("residents.xml");
            List<ResidentType> residenttypes = new List<ResidentType>();
            GlobalIndexes.last_residentType_index = 0;
            bool end = false;
            while (!end)
            {
                string resTypeName = "";
                List<Resident> residents = new List<Resident>();

                Console.Clear();
                Console.WriteLine($"Додано {residenttypes.Count} типів мешканців. Впишіть end щоб завершити.");
                Console.WriteLine();
                Console.Write("Введіть назву типу мешканця або end, щоб завершити: ");
                string Name = Console.ReadLine();
                if (Name != "end")
                {
                    resTypeName = Name;
                    while (!end)
                    {
                        int numberRes = 0;
                        Console.Clear();
                        Console.WriteLine($"Додайте до типу {resTypeName} мешканців. Щоб додати мешканця, впишіть його номер. Щоб завершити, напишіть end");
                        Console.WriteLine();
                        foreach (Resident res in existResidents) Console.WriteLine(res.ResidentId + ". " + res.Name);
                        Console.WriteLine();
                        Console.WriteLine($"Додані мешкаці: {String.Join(", ", residents.Select(r => r.Name))}");
                        Console.WriteLine();
                        Console.Write("Додати мешканця: ");
                        string input = Console.ReadLine();
                        if (string.IsNullOrEmpty(input)) continue;
                        if (input == "end")
                        {
                            end = true;
                            break;
                        }
                        if (!int.TryParse(input, out numberRes))
                        {
                            Console.Clear();
                            Console.WriteLine($"Додайте до типу {resTypeName} мешканців. Щоб додати мешканця, впишіть його номер.");
                            Console.WriteLine();
                            foreach (Resident res in existResidents) Console.WriteLine(res.ResidentId + ". " + res.Name);
                            Console.WriteLine();
                            Console.WriteLine($"Додані мешкаці: {String.Join(", ", residents.Select(r => r.Name))}");
                            Console.WriteLine();
                            Console.Write("Помилка. Некоректно введений номер. Додати мешканця: ");
                        }
                        if (numberRes >= 0 && numberRes < existResidents.Count && !residents.Any(r => r.ResidentId == numberRes)) residents.Add(existResidents.Where(n => n.ResidentId == numberRes).FirstOrDefault());
                    }
                    end = false;
                    ResidentType restype = new ResidentType(resTypeName);
                    restype.AddListResidents(residents);
                    residenttypes.Add(restype);
                }
                else end = true;
            }
            ResidentType.WriteToXmlFile(residenttypes, "residenttypes.xml");
            Console.WriteLine($"XML файл було успішно створено");
            Console.ReadLine();
            Console.Clear();
        }
        public static void AddNewResidentTypesByConsole()
        {
            List<Resident> existResidents = XMLmethods.XMLToListUsingSerializer<Resident>("residents.xml");
            List<ResidentType> existResidentTypes = XMLmethods.XMLToListUsingSerializer<ResidentType>("residenttypes.xml");
            List<ResidentType> residentTypesToAdd = new List<ResidentType>();
            GlobalIndexes.last_residentType_index = existResidentTypes.Count;
            bool end = false;
            while (!end)
            {
                string resTypeName = "";
                List<Resident> residents = new List<Resident>();

                Console.Clear();
                Console.WriteLine($"Додано {residentTypesToAdd.Count} типів мешканців. Впишіть end щоб завершити.");
                Console.WriteLine();
                Console.Write("Введіть назву типу мешканця або end, щоб завершити: ");
                string Name = Console.ReadLine();
                if (Name != "end")
                {
                    resTypeName = Name;
                    while (!end)
                    {
                        int numberRes = 0;
                        Console.Clear();
                        Console.WriteLine($"Додайте до типу {resTypeName} мешканців. Щоб додати мешканця, впишіть його номер. Щоб завершити, напишіть end");
                        Console.WriteLine();
                        foreach (Resident res in existResidents) Console.WriteLine(res.ResidentId + ". " + res.Name);
                        Console.WriteLine();
                        Console.WriteLine($"Додані мешкаці: {String.Join(", ", residents.Select(r => r.Name))}");
                        Console.WriteLine();
                        Console.Write("Додати мешканця: ");
                        string input = Console.ReadLine();
                        if (string.IsNullOrEmpty(input)) continue;
                        if (input == "end")
                        {
                            end = true;
                            break;
                        }
                        if (!int.TryParse(input, out numberRes))
                        {
                            Console.Clear();
                            Console.WriteLine($"Додайте до типу {resTypeName} мешканців. Щоб додати мешканця, впишіть його номер.");
                            Console.WriteLine();
                            foreach (Resident res in existResidents) Console.WriteLine(res.ResidentId + ". " + res.Name);
                            Console.WriteLine();
                            Console.WriteLine($"Додані мешкаці: {String.Join(", ", residents.Select(r => r.Name))}");
                            Console.WriteLine();
                            Console.Write("Помилка. Некоректно введений номер. Додати мешканця: ");
                        }
                        if (numberRes >= 0 && numberRes < existResidents.Count && !residents.Any(r => r.ResidentId == numberRes)) residents.Add(existResidents.Where(n => n.ResidentId == numberRes).FirstOrDefault());
                    }
                    end = false;
                    ResidentType restype = new ResidentType(resTypeName);
                    restype.AddListResidents(residents);
                    residentTypesToAdd.Add(restype);
                }
                else end = true;
            }
            existResidentTypes.AddRange(residentTypesToAdd);
            ResidentType.WriteToXmlFile(existResidentTypes, "residenttypes.xml");
            Console.WriteLine($"XML файл було успішно створено");
            Console.ReadLine();
            Console.Clear();
        }

        public static void PrintAllResidentTypesUsingSerializer()
        {
            List<ResidentType> residentTypes = XMLmethods.XMLToListUsingSerializer<ResidentType>("residenttypes.xml");
            foreach(var restype in residentTypes)
            {
                Console.WriteLine($"{restype.ResidentTypeId}. {restype.Name}");
                Console.WriteLine("Мешканці цього типу:");
                foreach(var resident in restype.Residents)
                {
                    Console.WriteLine($" - {resident.Name}");
                }
                Console.WriteLine();
            }
        }
        public static void PrintAllResidentTypesUsingDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load("residenttypes.xml");

                XmlNodeList residentTypeNodes = xmlDoc.SelectNodes("//ResidentType");

                foreach (XmlNode residentTypeNode in residentTypeNodes)
                {
                    XmlNode nameNode = residentTypeNode.SelectSingleNode("Name");
                    string name = nameNode.InnerText;

                    XmlNode residentTypeIdNode = residentTypeNode.SelectSingleNode("ResidentTypeId");
                    string residentTypeId = residentTypeIdNode.InnerText;

                    Console.WriteLine($"{residentTypeId}. {name}");

                    XmlNodeList residentNodes = residentTypeNode.SelectNodes("Residents/Resident");
                    Console.WriteLine("Мешканці цього типу:");
                    foreach (XmlNode residentNode in residentNodes)
                    {
                        XmlNode residentNameNode = residentNode.SelectSingleNode("Name");
                        string residentName = residentNameNode.InnerText;

                        Console.WriteLine($" - {residentName}");
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading XML file: {e.Message}");
            }
        }
    }
}
