using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace CitiesInfo
{
    public class JSONrequests
    {
        public static void DisplayCitiesSortedByFoundationYear()
        {
            try
            {
                using (FileStream fs = File.OpenRead("cities.json"))
                using (JsonDocument doc = JsonDocument.Parse(fs))
                {
                    JsonElement root = doc.RootElement;
                    if (root.ValueKind != JsonValueKind.Array)
                    {
                        Console.WriteLine("Помилка.");
                        return;
                    }

                    var sortedCities = root.EnumerateArray()
                        .OrderBy(city => city.GetProperty("FoundationYear").GetInt32())
                        .Select(city => new
                        {
                            CityName = city.GetProperty("Name").GetString(),
                            FoundationYear = city.GetProperty("FoundationYear").GetInt32()
                        });

                    foreach (var city in sortedCities)
                    {
                        Console.WriteLine($"Місто: {city.CityName}, Рік заснування: {city.FoundationYear}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
        public static void PrintLanguagesForSpeaker(string speakerName)
        {
            try
            {
                string json = File.ReadAllText("languages.json");

                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;

                    foreach (JsonElement language in root.EnumerateArray())
                    {
                        foreach (JsonElement speaker in language.GetProperty("Speakers").EnumerateArray())
                        {
                            if (speaker.GetProperty("Name").ToString() == speakerName)
                            {
                                Console.WriteLine($"Мова: {language.GetProperty("Name")}");
                                break;
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Помилка: файл не знайдено.");
            }
            catch (JsonException)
            {
                Console.WriteLine("Помилка: неправильний формат JSON.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        public static void FindResidentById(int residentId)
        {
            try
            {
                string json = File.ReadAllText("residents.json");

                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;

                    foreach (JsonElement residentElement in root.EnumerateArray())
                    {
                        int residentIdValue = residentElement.GetProperty("ResidentId").GetInt32();

                        if (residentIdValue == residentId)
                        {
                            string name = residentElement.GetProperty("Name").GetString();

                            Console.WriteLine($"Мешканець з id {residentIdValue} - {name}");
                        }
                    }
                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Помилка: файл не знайдено.");
            }
            catch (JsonException)
            {
                Console.WriteLine("Помилка: неправильний формат JSON.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
        public static void PrintResidentTypesDescending()
        {
            try
            {
                string json = File.ReadAllText("restypes.json");

                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;

                    var residentTypeCounts = new Dictionary<string, int>();

                    foreach (JsonElement residentTypeElement in root.EnumerateArray())
                    {
                        string typeName = residentTypeElement.GetProperty("Name").GetString();

                        int residentCount = residentTypeElement.GetProperty("Residents").GetArrayLength();

                        residentTypeCounts.Add(typeName, residentCount);
                    }

                    var sortedCounts = residentTypeCounts.OrderByDescending(x => x.Value);

                    foreach (var item in sortedCounts)
                    {
                        Console.WriteLine($"{item.Key}: {item.Value}");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Помилка: файл не знайдено.");
            }
            catch (JsonException)
            {
                Console.WriteLine("Помилка: неправильний формат JSON.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}
