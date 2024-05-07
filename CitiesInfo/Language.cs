using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CitiesInfo
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public List<Resident> Speakers { get; set; }

        public Language(string Name)
        {
            this.LanguageId = GlobalIndexes.last_language_index++;
            this.Name = Name;
            this.Speakers = new List<Resident>();
        }
        public Language()
        {
        }

        public static void AddLanguagesToResident(string residentName, List<Resident> allResidents, List<Language> allLanguages, string[] nameLanguagesToAdd)
        {
            Resident resident = allResidents.FirstOrDefault(res => res.Name == residentName);

            if (resident != null)
            {
                foreach(string lan in nameLanguagesToAdd)
                {
                    if (allLanguages.Any(l => l.Name == lan)) allLanguages.FirstOrDefault(l => l.Name == lan).AddResident(resident);
                    else Console.WriteLine($"Виникла помилка. Мови {lan} не існує");
                }
            }
            else Console.WriteLine($"Людина з ім'ям '{residentName}' не знайдена.");
        }

        public void AddResident(Resident resident)
        {
            this.Speakers.Add(resident);
        }

        public static void WriteToXmlFile(List<Language> languages, string filePath)
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
                writer.WriteStartElement("ArrayOfLanguage");

                foreach (var language in languages)
                {
                    writer.WriteStartElement("Language");

                    writer.WriteElementString("LanguageId", language.LanguageId.ToString());
                    writer.WriteElementString("Name", language.Name);

                    writer.WriteStartElement("Speakers");
                    foreach (var speaker in language.Speakers)
                    {
                        writer.WriteStartElement("Resident");
                        writer.WriteElementString("ResidentId", speaker.ResidentId.ToString());
                        writer.WriteElementString("Name", speaker.Name);
                        writer.WriteEndElement(); 
                    }
                    writer.WriteEndElement(); 

                    writer.WriteEndElement();
                }

                writer.WriteEndElement(); 
                writer.WriteEndDocument();
            }
        }
        public static void CreateNewLanguagesByConsole()
        {
            List<Resident> existResidents = XMLmethods.XMLToListUsingSerializer<Resident>("residents.xml");

            List<Language> languages = new List<Language>();
            GlobalIndexes.last_language_index = 0;
            bool end = false;
            while (!end)
            {
                Console.Clear();
                Console.WriteLine($"Додано {languages.Count} мов. Впишіть end щоб завершити.");
                Console.WriteLine();
                Console.Write("Введіть назву мови або end, щоб завершити: ");
                string Name = Console.ReadLine();
                if (Name != "end")
                {
                    Language language = new Language(Name);
                    languages.Add(language);
                }
                else end = true;
            }
            end = false;
            while (!end)
            {
                int numberRes = -1;
                int lanId = -1;

                List<Language> reslanguages = new List<Language>();
                Console.Clear();
                Console.WriteLine("Додайте мешканцям мови, якими вони розмовляють");
                Console.WriteLine();
                foreach (Resident res in existResidents) Console.WriteLine(res.ResidentId + ". " + res.Name);
                Console.WriteLine();
                Console.Write("Введіть номер мешканця для додавання йому мов або впишіть end, щоб закінчити: ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;
                if (input == "end")
                {
                    end = true;
                    break;
                }
                if (int.TryParse(input, out numberRes))
                {
                    if (numberRes >= 0 && numberRes < existResidents.Count)
                    {
                        while (!end)
                        {
                            Console.Clear();
                            Console.WriteLine($"Додавання мов мешканцю {existResidents[numberRes].Name}");
                            Console.WriteLine();
                            foreach (var lan in languages) Console.WriteLine(lan.LanguageId + ". " + lan.Name);
                            Console.WriteLine();
                            Console.WriteLine($"Додані мови: {String.Join(", ", reslanguages.Select(r => r.Name))}");
                            Console.WriteLine();
                            Console.Write("Впишіть номер мови, яку ви хочете додати. Щоб закінчити впишіть end: ");
                            input = Console.ReadLine();
                            if (string.IsNullOrEmpty(input)) continue;
                            if (input == "end")
                            {
                                end = true;
                                break;
                            }
                            if (int.TryParse(input, out lanId))
                            {
                                if (lanId >= 0 && lanId < languages.Count && !reslanguages.Any(r => r.LanguageId == lanId)) reslanguages.Add(languages[lanId]);
                            }
                        }
                        end = false;
                        foreach (var lan in reslanguages)
                        {
                            languages.Where(language => language.LanguageId == lan.LanguageId).FirstOrDefault().Speakers.Add(existResidents[numberRes]);
                        }

                    }

                }
               
            }
            Language.WriteToXmlFile(languages, "languages.xml");
            Console.WriteLine($"XML файл було успішно створено");
            Console.ReadLine();
            Console.Clear();
        }
        public static void AddNewLanguagesByConsole()
        {

            List<Resident> existResidents = XMLmethods.XMLToListUsingSerializer<Resident>("residents.xml");
            List<Language> existLanguages = XMLmethods.XMLToListUsingSerializer<Language>("languages.xml");
            List<Language> languagesToAdd = new List<Language>();
            GlobalIndexes.last_language_index = existLanguages.Count;
            bool end = false;
            while (!end)
            {
                Console.Clear();
                Console.WriteLine($"Додано {languagesToAdd.Count} мов. Впишіть end щоб завершити.");
                Console.WriteLine();
                Console.Write("Введіть назву мови або end, щоб завершити: ");
                string Name = Console.ReadLine();
                if (Name != "end")
                {
                    Language language = new Language(Name);
                    languagesToAdd.Add(language);
                }
                else end = true;
            }
            end = false;
            while (!end)
            {
                int numberRes = -1;
                int lanId = -1;

                List<Language> reslanguages = new List<Language>();
                Console.Clear();
                Console.WriteLine("Додайте мешканцям мови, якими вони розмовляють");
                Console.WriteLine();
                foreach (Resident res in existResidents) Console.WriteLine(res.ResidentId + ". " + res.Name);
                Console.WriteLine();
                Console.Write("Введіть номер мешканця для додавання йому мов або впишіть end, щоб закінчити: ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;
                if (input == "end")
                {
                    end = true;
                    break;
                }
                if (int.TryParse(input, out numberRes))
                {
                    if (numberRes >= 0 && numberRes < existResidents.Count)
                    {
                        while (!end)
                        {
                            Console.Clear();
                            Console.WriteLine($"Додавання мов мешканцю {existResidents[numberRes].Name}");
                            Console.WriteLine();
                            foreach (var lan in languagesToAdd) Console.WriteLine(lan.LanguageId + ". " + lan.Name);
                            Console.WriteLine();
                            Console.WriteLine($"Додані мови: {String.Join(", ", reslanguages.Select(r => r.Name))}");
                            Console.WriteLine();
                            Console.Write("Впишіть номер мови, яку ви хочете додати. Щоб закінчити впишіть end: ");
                            input = Console.ReadLine();
                            if (string.IsNullOrEmpty(input)) continue;
                            if (input == "end")
                            {
                                end = true;
                                break;
                            }
                            if (int.TryParse(input, out lanId))
                            {
                                if (lanId >= languagesToAdd[0].LanguageId && lanId <= languagesToAdd[languagesToAdd.Count - 1].LanguageId && !reslanguages.Any(r => r.LanguageId == lanId)) reslanguages.Add(languagesToAdd.Where(l => l.LanguageId == lanId).FirstOrDefault());
                            }
                        }
                        end = false;
                        foreach (var lan in reslanguages)
                        {
                            languagesToAdd.Where(language => language.LanguageId == lan.LanguageId).FirstOrDefault().Speakers.Add(existResidents[numberRes]);
                        }

                    }

                }

            }
            existLanguages.AddRange(languagesToAdd);
            Language.WriteToXmlFile(existLanguages, "languages.xml");
            Console.WriteLine($"XML файл було успішно створено");
            Console.ReadLine();
            Console.Clear();
        }

        public static void PrintAllLanguagesUsingSerialiser()
        {
            List<Language> languages = XMLmethods.XMLToListUsingSerializer<Language>("languages.xml");
            foreach (var language in languages)
            {
                Console.WriteLine($"{language.LanguageId}. {language.Name}");
                Console.WriteLine($"Люди, які спілкуються цією мовою");
                foreach(var res in language.Speakers)
                {
                    Console.WriteLine($" - {res.Name}");
                }
                Console.WriteLine();
            }
        }
        public static void PrintAllLanguagesUsingDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load("languages.xml");

                XmlNodeList languageNodes = xmlDoc.SelectNodes("//Language");

                foreach (XmlNode languageNode in languageNodes)
                {
                    XmlNode nameNode = languageNode.SelectSingleNode("Name");
                    string name = nameNode.InnerText;

                    XmlNode languageIdNode = languageNode.SelectSingleNode("LanguageId");
                    string languageId = languageIdNode.InnerText;

                    Console.WriteLine($"{languageId}. {name}");
                    Console.WriteLine($"Люди, які спілкуються цією мовою");

                    XmlNodeList speakerNodes = languageNode.SelectNodes("Speakers/Resident");
                    foreach (XmlNode speakerNode in speakerNodes)
                    {
                        XmlNode speakerNameNode = speakerNode.SelectSingleNode("Name");
                        string speakerName = speakerNameNode.InnerText;

                        Console.WriteLine($" - {speakerName}");
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
