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
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int FoundationYear { get; set; }
        public double Area { get; set; }
        public string Country { get; set; }

        public List<Resident> Residents { get; set; }

        public City()
        {
        }

        public City(string Name, int FundationYear, double Area, string Country) 
        {
            this.CityId = GlobalIndexes.last_city_index++;
            this.Name = Name;
            this.FoundationYear = FundationYear;
            this.Area = Area;
            this.Country = Country;
            this.Residents = new List<Resident>();
        }
        public static void AddResidentsToCity(string cityName, List<City> allCities, List<Resident> allResidents, string[] nameResidentsToAdd)
        {
            City city = allCities.FirstOrDefault(c => c.Name == cityName);
            if(city != null)
            {
                foreach(string resident in nameResidentsToAdd)
                {
                    if (allResidents.Any(c => c.Name == resident)) city.AddResident(allResidents.FirstOrDefault(c => c.Name == resident));
                    else Console.WriteLine($"Виникла помилка. Людини з ім'ям {resident} не існує");
                }
            }
            else Console.WriteLine($"Місто з назвою '{cityName}' не знайдена.");
        }



        private void AddListResidents(List<Resident> residents)
        {
            this.Residents = residents;
        }
        public void AddResident(Resident resident)
        {
            this.Residents.Add(resident);
        }

        public static void WriteToXmlFile(List<City> cities, string filePath)
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
                writer.WriteStartElement("ArrayOfCity");

                foreach (var city in cities)
                {
                    writer.WriteStartElement("City");

                    writer.WriteElementString("CityId", city.CityId.ToString());
                    writer.WriteElementString("Name", city.Name);
                    writer.WriteElementString("FoundationYear", city.FoundationYear.ToString(cultureInfo));
                    writer.WriteElementString("Area", city.Area.ToString(cultureInfo)); 
                    writer.WriteElementString("Country", city.Country);

                    writer.WriteStartElement("Residents");
                    foreach (var resident in city.Residents)
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


        public static void CreateNewCitiesByConsole()
        {
            List<City> cities = new List<City>();
            GlobalIndexes.last_city_index = 0;
            bool end = false;
            while (!end)
            {
                string cityName = "";
                int foundYear = 0;
                string country = "";
                double area = 0;

                List<Resident> residents = new List<Resident>();

                Console.Clear();
                Console.WriteLine($"Додано {cities.Count} міст. Впишіть end щоб завершити.");
                Console.WriteLine();
                Console.Write("Введіть назву міста або end, щоб завершити: ");
                string Name = Console.ReadLine();
                if (Name != "end")
                {
                    cityName = Name;
                    Console.Clear();
                    Console.Write($"Введіть рік заснування міста {cityName}: ");
                    while (!int.TryParse(Console.ReadLine(), out foundYear))
                    {
                        Console.Clear();
                        Console.Write($"Помилка. Введіть коректно заснування міста {cityName}: ");
                    }

                    Console.Clear();
                    Console.Write($"Введіть країну міста {cityName}: ");
                    country = Console.ReadLine();

                    Console.Clear();
                    Console.Write($"Введіть площу міста {cityName}: ");
                    while (!double.TryParse(Console.ReadLine(), out area))
                    {
                        Console.Clear();
                        Console.Write($"Помилка. Введіть коректно площу міста {cityName}: ");
                    }

                    List<Resident> existResidents = XMLmethods.XMLToListUsingSerializer<Resident>("residents.xml");

                    while (!end)
                    {
                        int numberRes = -1;
                        Console.Clear();
                        Console.WriteLine($"Додайте до міста {cityName} мешканців. Щоб додати мешканця, впишіть його номер. Щоб завершити, напишіть end");
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
                            Console.WriteLine($"Додайте до міста {cityName} мешканців. Щоб додати мешканця, впишіть його номер.");
                            Console.WriteLine();
                            foreach (Resident res in existResidents) Console.WriteLine(res.ResidentId + ". " + res.Name);
                            Console.WriteLine();
                            Console.WriteLine($"Додані мешкаці: {String.Join(", ", residents.Select(r => r.Name))}");
                            Console.WriteLine();
                            Console.Write("Помилка. Некоректно введений номер. Додати мешканця: ");
                        }
                        if (numberRes>=0 && numberRes< existResidents.Count && !residents.Any(r => r.ResidentId == numberRes) && int.TryParse(input, out numberRes)) residents.Add(existResidents.Where(n => n.ResidentId == numberRes).FirstOrDefault());
                    }
                    end = false;
                    City city = new City(cityName, foundYear, area, country);
                    city.AddListResidents(residents);
                    cities.Add(city);
                }
                else end = true;
            }
            City.WriteToXmlFile(cities, "cities.xml");
            Console.WriteLine($"XML файл було успішно створено");
            Console.ReadLine();
            Console.Clear();
        }
        public static void AddNewCitiesByConsole()
        {
            List<City> existCities = XMLmethods.XMLToListUsingSerializer<City>("cities.xml");
            List<City> citiesToAdd = new List<City>();
            GlobalIndexes.last_city_index = existCities.Count;
            bool end = false;
            while (!end)
            {
                string cityName = "";
                int foundYear = 0;
                string country = "";
                double area = 0;

                List<Resident> residents = new List<Resident>();

                Console.Clear();
                Console.WriteLine($"Додано {citiesToAdd.Count} міст. Впишіть end щоб завершити.");
                Console.WriteLine();
                Console.Write("Введіть назву міста або end, щоб завершити: ");
                string Name = Console.ReadLine();
                if (Name != "end")
                {
                    cityName = Name;
                    Console.Clear();
                    Console.Write($"Введіть рік заснування міста {cityName}: ");
                    while (!int.TryParse(Console.ReadLine(), out foundYear))
                    {
                        Console.Clear();
                        Console.Write($"Помилка. Введіть коректно заснування міста {cityName}: ");
                    }

                    Console.Clear();
                    Console.Write($"Введіть країну міста {cityName}: ");
                    country = Console.ReadLine();

                    Console.Clear();
                    Console.Write($"Введіть площу міста {cityName}: ");
                    while (!double.TryParse(Console.ReadLine(), out area))
                    {
                        Console.Clear();
                        Console.Write($"Помилка. Введіть коректно площу міста {cityName}: ");
                    }

                    List<Resident> existResidents = XMLmethods.XMLToListUsingSerializer<Resident>("residents.xml");

                    while (!end)
                    {
                        int numberRes = -1;
                        Console.Clear();
                        Console.WriteLine($"Додайте до міста {cityName} мешканців. Щоб додати мешканця, впишіть його номер. Щоб завершити, напишіть end");
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
                            Console.WriteLine($"Додайте до міста {cityName} мешканців. Щоб додати мешканця, впишіть його номер.");
                            Console.WriteLine();
                            foreach (Resident res in existResidents) Console.WriteLine(res.ResidentId + ". " + res.Name);
                            Console.WriteLine();
                            Console.WriteLine($"Додані мешкаці: {String.Join(", ", residents.Select(r => r.Name))}");
                            Console.WriteLine();
                            Console.Write("Помилка. Некоректно введений номер. Додати мешканця: ");
                        }
                        if (numberRes >= 0 && numberRes < existResidents.Count && !residents.Any(r => r.ResidentId == numberRes) && int.TryParse(input, out numberRes)) residents.Add(existResidents.Where(n => n.ResidentId == numberRes).FirstOrDefault());
                    }
                    end = false;
                    City city = new City(cityName, foundYear, area, country);
                    city.AddListResidents(residents);
                    citiesToAdd.Add(city);
                }
                else end = true;
            }
            existCities.AddRange(citiesToAdd);
            City.WriteToXmlFile(existCities, "cities.xml");
            Console.WriteLine($"XML файл було успішно створено");
            Console.ReadLine();
            Console.Clear();
        }

        public static void PrintAllCitiesUsingSerializer()
        {
            List<City> cities = XMLmethods.XMLToListUsingSerializer<City>("cities.xml");
            foreach(var city  in cities)
            {
                Console.WriteLine($"{city.CityId}. {city.Name}");
                Console.WriteLine($"Дата заснування: {city.FoundationYear}");
                Console.WriteLine($"Країна: {city.Country}");
                Console.WriteLine($"Площа: {city.Area}");
                Console.WriteLine($"Мешканці:");
                foreach(var resident in city.Residents)
                {
                    Console.WriteLine($" - {resident.Name}");
                }
                Console.WriteLine();

            }
        }
        public static void PrintAllCitiesUsingDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load("cities.xml");
                XmlNodeList cityNodes = xmlDoc.SelectNodes("//City");
                foreach (XmlNode cityNode in cityNodes)
                {
                    XmlNode nameNode = cityNode.SelectSingleNode("Name");
                    string name = nameNode.InnerText;

                    XmlNode cityIdNode = cityNode.SelectSingleNode("CityId");
                    string cityId = cityIdNode.InnerText;

                    XmlNode foundationYearNode = cityNode.SelectSingleNode("FoundationYear");
                    string foundationYear = foundationYearNode.InnerText;

                    XmlNode areaNode = cityNode.SelectSingleNode("Area");
                    string area = areaNode.InnerText;

                    XmlNode countryNode = cityNode.SelectSingleNode("Country");
                    string country = countryNode.InnerText;

                    Console.WriteLine($"{cityId}. {name}");
                    Console.WriteLine($"Дата заснування: {foundationYear}");
                    Console.WriteLine($"Країна: {country}");
                    Console.WriteLine($"Площа: {area}");

                    XmlNodeList residentNodes = cityNode.SelectNodes("Residents/Resident");
                    if (residentNodes.Count > 0)
                    {
                        Console.WriteLine("Мешканці:");
                        foreach (XmlNode residentNode in residentNodes)
                        {
                            XmlNode residentNameNode = residentNode.SelectSingleNode("Name");
                            string residentName = residentNameNode.InnerText;
                            Console.WriteLine($" - {residentName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Мешканці відсутні");
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
