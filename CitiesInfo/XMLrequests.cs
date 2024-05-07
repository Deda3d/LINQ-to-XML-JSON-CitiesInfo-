using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CitiesInfo
{
    public class XMLrequests
    {
        static XDocument cities_doc = XDocument.Load("cities.xml");
        static XDocument residents_doc = XDocument.Load("residents.xml");
        static XDocument languages_doc = XDocument.Load("languages.xml");
        static XDocument residenttypes_doc = XDocument.Load("residenttypes.xml");

        // Метод для виводу інформації про міста та їх мешканців +++
        public static void DisplayCitiesAndResidents()
        {
            var citiesAndResidents = from city in XMLrequests.cities_doc.Descendants("City")
                                     select new
                                     {
                                         CityName = city.Element("Name").Value,
                                         Residents = city.Descendants("Resident").Select(r => r.Element("Name").Value).ToList()
                                     };

            foreach (var city in citiesAndResidents)
            {
                Console.WriteLine($"Місто: {city.CityName}");
                Console.WriteLine("Мешканці:");
                foreach (var resident in city.Residents)
                {
                    Console.WriteLine($"- {resident}");
                }
                Console.WriteLine();
            }
        }

        // Метод для сортування міст за роком заснування +++
        public static void DisplayCitiesSortedByFoundationYear()
        {
            var sortedCities = from city in cities_doc.Descendants("City")
                               orderby int.Parse(city.Element("FoundationYear").Value)
                               select new
                               {
                                   CityName = city.Element("Name").Value,
                                   FoundationYear = city.Element("FoundationYear").Value
                               };

            foreach (var city in sortedCities)
            {
                Console.WriteLine($"Місто: {city.CityName}, Рік заснування: {city.FoundationYear}");
            }
        }

        // Метод для пошуку міст за ідентифікатором +++
        public static void FindCityById(int cityId)
        {
            var city = cities_doc.Descendants("City")
                          .Where(c => int.Parse(c.Element("CityId").Value) == cityId)
                          .Select(c => new
                          {
                              CityName = c.Element("Name").Value,
                              FoundationYear = c.Element("FoundationYear").Value
                          })
                          .FirstOrDefault();

            if (city != null)
            {
                Console.WriteLine($"Місто: {city.CityName}, Рік заснування: {city.FoundationYear}");
            }
            else
            {
                Console.WriteLine("Місто не знайдено.");
            }
        }

        // Метод для вывода всех городов определенной страны +++
        public static void DisplayCitiesInCountry(string country)
        {
            var citiesInCountry = from city in cities_doc.Descendants("City")
                                  where city.Element("Country").Value == country
                                  select city.Element("Name").Value;

            Console.WriteLine($"Міста в країні {country}:");
            foreach (var cityName in citiesInCountry)
            {
                Console.WriteLine(cityName);
            }
        }


        // Метод для виводу мов та кількості носіїв цієї мови +++
        public static void DisplayLanguagesAndSpeakersCount()
        {
            var languagesAndSpeakers = from language in languages_doc.Descendants("Language")
                                       select new
                                       {
                                           LanguageName = language.Element("Name").Value,
                                           NumberOfSpeakers = language.Descendants("Resident").Count()
                                       };

            foreach (var language in languagesAndSpeakers)
            {
                Console.WriteLine($"Мова: {language.LanguageName}, Кількість носіїв: {language.NumberOfSpeakers}");
            }
        }

        // Вивести мови, якими розмовляє людина за ім'ям +++
        public static void DisplayLanguagesSpokenByResident(string residentName)
        {
            var languages = from language in languages_doc.Descendants("Language")
                            where language.Descendants("Resident").Any(resident => resident.Element("Name").Value == residentName)
                            select language.Element("Name").Value;

            Console.WriteLine($"Мови, на яких розмовляє {residentName}:");
            foreach (var language in languages)
            {
                Console.WriteLine(language);
            }
        }

        // Метод для пошуку мови за айді +++
        public static void FindLanguageById(int languageId)
        {
            var language = languages_doc.Descendants("Language")
                              .Where(l => int.Parse(l.Element("LanguageId").Value) == languageId)
                              .Select(l => l.Element("Name").Value)
                              .FirstOrDefault();

            if (language != null)
            {
                Console.WriteLine($"Мова з ідентифікатором {languageId}: {language}");
            }
            else
            {
                Console.WriteLine("Мова не знайдена.");
            }
        }

        // Метод для виводу всіх носїв за назвою мови +++
        public static void DisplaySpeakersOfLanguage(string languageName)
        {
            var speakers = from language in languages_doc.Descendants("Language")
                           where language.Element("Name").Value == languageName
                           from speaker in language.Descendants("Resident")
                           select speaker.Element("Name").Value;

            Console.WriteLine($"Носители языка {languageName}:");
            foreach (var speaker in speakers)
            {
                Console.WriteLine(speaker);
            }
        }


        // Метод для виводу мешканців за буквою +++
        public static void DisplayResidentsStartingWithLetter(char letter)
        {
            var residents = from resident in residents_doc.Descendants("Resident")
                            where resident.Element("Name").Value.StartsWith(letter.ToString(), StringComparison.OrdinalIgnoreCase)
                            select new
                            {
                                ResidentId = resident.Element("ResidentId").Value,
                                Name = resident.Element("Name").Value
                            };

            Console.WriteLine($"Мешканці на букву '{letter}':");
            foreach (var resident in residents)
            {
                Console.WriteLine($"ID: {resident.ResidentId}, Ім'я: {resident.Name}");
            }
        }

        // Метод для пошуку мешканця за ідентифікатром +++
        public static void FindResidentById(int residentId)
        {
            var resident = residents_doc.Descendants("Resident")
                              .Where(r => int.Parse(r.Element("ResidentId").Value) == residentId)
                              .Select(r => r.Element("Name").Value)
                              .FirstOrDefault();

            if (resident != null)
            {
                Console.WriteLine($"Мешканець з ідентифікатором {residentId}: {resident}");
            }
            else
            {
                Console.WriteLine("Мешканця не знайдено.");
            }
        }

        // Метод для виводу останнього мешканця списку +++
        public static void DisplayLastResident()
        {
            var lastResident = residents_doc.Descendants("Resident")
                                  .LastOrDefault();

            if (lastResident != null)
            {
                string residentId = lastResident.Element("ResidentId").Value;
                string name = lastResident.Element("Name").Value;
                Console.WriteLine($"Останній мешканець:");
                Console.WriteLine($"ID: {residentId}, Ім'я: {name}");
            }
            else
            {
                Console.WriteLine("Список мешканців порожній.");
            }
        }

        // Метод для виводу інформації про мешканця з найдовшим ім'ям +++
        public static void DisplayResidentWithLongestName()
        {
            var longestNameResident = residents_doc.Descendants("Resident")
                                         .OrderByDescending(r => r.Element("Name").Value.Length)
                                         .FirstOrDefault();

            if (longestNameResident != null)
            {
                string residentId = longestNameResident.Element("ResidentId").Value;
                string name = longestNameResident.Element("Name").Value;
                Console.WriteLine($"Мешканець з найдовшим ім'ям:");
                Console.WriteLine($"ID: {residentId}, Ім'я: {name}");
            }
            else
            {
                Console.WriteLine("Список мешканців порожній.");
            }
        }



        // Метод для виводу типів мешканців та їх кількості за спаданням +++
        public static void DisplayResidentTypesAndCountsDescending()
        {
            var typesAndCounts = from type in residenttypes_doc.Descendants("ResidentType")
                                 let residentCount = type.Descendants("Resident").Count()
                                 orderby residentCount descending
                                 select new
                                 {
                                     TypeName = type.Element("Name").Value,
                                     ResidentCount = residentCount
                                 };

            Console.WriteLine("Типи мешканців та кількість мешканів цього типу (за спаданням):");
            foreach (var typeAndCount in typesAndCounts)
            {
                Console.WriteLine($"Тип: {typeAndCount.TypeName}, Кількість мешканців: {typeAndCount.ResidentCount}");
            }
        }



        // Метод для виводу типу мешканця за іденитфікатором
        public static void FindResidentTypeById(int typeId)
        {
            var residentType = residenttypes_doc.Descendants("ResidentType")
                                  .Where(t => int.Parse(t.Element("ResidentTypeId").Value) == typeId)
                                  .Select(t => t.Element("Name").Value)
                                  .FirstOrDefault();

            if (residentType != null)
            {
                Console.WriteLine($"Тип мешканця з ідентифікатором {typeId}: {residentType}");
            }
            else
            {
                Console.WriteLine("Тип жителя не знайдено.");
            }
        }





        public static void DisplayResidentsOfResidentType(string typeName)
        {
            var residents = from type in residenttypes_doc.Descendants("ResidentType")
                            where type.Element("Name").Value == typeName
                            from resident in type.Descendants("Resident")
                            select new
                            {
                                ResidentId = resident.Element("ResidentId").Value,
                                Name = resident.Element("Name").Value
                            };

            Console.WriteLine($"Жителі типу '{typeName}':");
            foreach (var resident in residents)
            {
                Console.WriteLine($"ID: {resident.ResidentId}, Имя: {resident.Name}");
            }
        }








    }
}
