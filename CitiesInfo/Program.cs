using CitiesInfo;
using CitiesInfo.ConsoleMenu;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {

        Console.OutputEncoding = Encoding.UTF8;

        // Створюємо список жителів
        List<Resident> residents = new List<Resident>
        {
            new Resident("Олександр"),
            new Resident("Марія"),
            new Resident("Іван"),
            new Resident("Анна"),
            new Resident("Петро"),
            new Resident("Олексій"),
            new Resident("Михайло"),
            new Resident("Наталія"),
            new Resident("Олег"),
            new Resident("Тетяна"),
            new Resident("Світлана"),
            new Resident("Артем"),
            new Resident("Оксана"),
            new Resident("Сергій"),
            new Resident("Ірина"),
            new Resident("Дмитро"),
            new Resident("Олена"),
            new Resident("Віктор"),
            new Resident("Катерина"),
            new Resident("Роман")
        };

        // Створюємо список міст
        List<City> cities = new List<City>
        {
            new City("Київ", 482, 839.9, "Україна"),
            new City("Львів", 1256, 182.01, "Україна"),
            new City("Одеса", 1794, 162.42, "Україна"),
            new City("Дніпро", 1776, 405, "Україна"),
            new City("Харків", 1654, 350, "Україна"),
            new City("Варшава", 1300, 517.24, "Польща"),
            new City("Краків", 700, 326.8, "Польща"),
            new City("Вроцлав", 1000, 292.82, "Польща"),
            new City("Познань", 966, 261.85, "Польща"),
            new City("Гданськ", 997, 262, "Польща"),
            new City("Будапешт", 897, 525.13, "Угорщина"),
            new City("Дебрецен", 1235, 461.25, "Угорщина"),
            new City("Сегед", 1247, 280.84, "Угорщина"),
            new City("Мішкольц", 1365, 224.66, "Угорщина"),
            new City("Печ", 1361, 162.61, "Угорщина"),
            new City("Прага", 885, 496, "Чехія"),
            new City("Брно", 1000, 230, "Чехія"),
            new City("Острава", 1267, 214, "Чехія"),
            new City("Пльзень", 1295, 137.65, "Чехія"),
            new City("Ліберець", 1348, 106.09, "Чехія")
        };

        // Створюємо список мов
        List<Language> languages = new List<Language>
        {
            new Language("Українська"),
            new Language("Англійська"),
            new Language("Іспанська"),
            new Language("Французька"),
            new Language("Німецька"),
            new Language("Італійська"),
            new Language("Португальська"),
            new Language("Румунська"),
            new Language("Китайська"),
            new Language("Японська"),
            new Language("Турецька"),
            new Language("Корейська"),
            new Language("Арабська"),
            new Language("Гінді"),
            new Language("Угорська"),
            new Language("Польська"),
            new Language("Нідерландська"),
            new Language("Грецька"),
            new Language("Чеська"),
            new Language("Шведська")
        };

        // Створюємо список типів мешканців
        List<ResidentType> residentTypes = new List<ResidentType>
        {
            new ResidentType("Швачка"),
            new ResidentType("ІТ-спеціаліст"),
            new ResidentType("Вчитель"),
            new ResidentType("Медичний працівник"),
            new ResidentType("Підприємець"),
            new ResidentType("Інженер"),
            new ResidentType("Науковець"),
            new ResidentType("Художник"),
            new ResidentType("Музикант"),
            new ResidentType("Письменник"),
            new ResidentType("Юрист"),
            new ResidentType("Архітектор"),
            new ResidentType("Кухар"),
            new ResidentType("Фермер"),
            new ResidentType("Студент-медик"),
            new ResidentType("Студент-інженер"),
            new ResidentType("Студент-економіст"),
            new ResidentType("Студент-філолог"),
            new ResidentType("Студент-правознавець"),
            new ResidentType("Студент-історик")
        };

        //Створення зв'язків
        {
            Language.AddLanguagesToResident("Олександр", residents, languages, new string[] { "Українська", "Англійська", "Шведська" });
            Language.AddLanguagesToResident("Марія", residents, languages, new string[] { "Італійська", "Німецька" });
            Language.AddLanguagesToResident("Іван", residents, languages, new string[] { "Французька" });
            Language.AddLanguagesToResident("Анна", residents, languages, new string[] { "Іспанська", "Португальська" });
            Language.AddLanguagesToResident("Петро", residents, languages, new string[] { "Англійська", "Китайська" });
            Language.AddLanguagesToResident("Олексій", residents, languages, new string[] { "Румунська" });
            Language.AddLanguagesToResident("Михайло", residents, languages, new string[] { "Українська", "Англійська" });
            Language.AddLanguagesToResident("Наталія", residents, languages, new string[] { "Німецька", "Англійська" });
            Language.AddLanguagesToResident("Олег", residents, languages, new string[] { "Італійська", "Французька" });
            Language.AddLanguagesToResident("Тетяна", residents, languages, new string[] { "Іспанська", "Польська" });
            Language.AddLanguagesToResident("Світлана", residents, languages, new string[] { "Англійська" });
            Language.AddLanguagesToResident("Артем", residents, languages, new string[] { "Українська", "Німецька" });
            Language.AddLanguagesToResident("Оксана", residents, languages, new string[] { "Французька" });
            Language.AddLanguagesToResident("Сергій", residents, languages, new string[] { "Англійська", "Італійська" });
            Language.AddLanguagesToResident("Ірина", residents, languages, new string[] { "Українська" });
            Language.AddLanguagesToResident("Дмитро", residents, languages, new string[] { "Англійська", "Корейська" });
            Language.AddLanguagesToResident("Олена", residents, languages, new string[] { "Німецька", "Англійська" });
            Language.AddLanguagesToResident("Віктор", residents, languages, new string[] { "Італійська", "Угорська" });
            Language.AddLanguagesToResident("Катерина", residents, languages, new string[] { "Англійська", "Португальська" });
            Language.AddLanguagesToResident("Роман", residents, languages, new string[] { "Іспанська" });

            City.AddResidentsToCity("Київ", cities, residents, new string[] { "Олександр", });
            City.AddResidentsToCity("Львів", cities, residents, new string[] { "Марія", "Іван" });
            City.AddResidentsToCity("Одеса", cities, residents, new string[] { });
            City.AddResidentsToCity("Дніпро", cities, residents, new string[] { "Анна", "Петро" });
            City.AddResidentsToCity("Харків", cities, residents, new string[] { "Олексій" });
            City.AddResidentsToCity("Варшава", cities, residents, new string[] { "Михайло", "Наталія", "Олег", "Тетяна" });
            City.AddResidentsToCity("Краків", cities, residents, new string[] { });
            City.AddResidentsToCity("Вроцлав", cities, residents, new string[] { });
            City.AddResidentsToCity("Познань", cities, residents, new string[] { "Світлана" });
            City.AddResidentsToCity("Гданськ", cities, residents, new string[] { });
            City.AddResidentsToCity("Будапешт", cities, residents, new string[] { "Артем", "Оксана" });
            City.AddResidentsToCity("Дебрецен", cities, residents, new string[] { });
            City.AddResidentsToCity("Сегед", cities, residents, new string[] { "Сергій", "Ірина", "Дмитро" });
            City.AddResidentsToCity("Мішкольц", cities, residents, new string[] { });
            City.AddResidentsToCity("Печ", cities, residents, new string[] { "Олена" });
            City.AddResidentsToCity("Прага", cities, residents, new string[] { });
            City.AddResidentsToCity("Брно", cities, residents, new string[] { "Віктор" });
            City.AddResidentsToCity("Острава", cities, residents, new string[] { });
            City.AddResidentsToCity("Пльзень", cities, residents, new string[] { "Катерина", "Роман" });
            City.AddResidentsToCity("Ліберець", cities, residents, new string[] { });

            ResidentType.AddResidentsToType("Швачка", residentTypes, residents, new string[] { "Роман", "Олег", "Тетяна" });
            ResidentType.AddResidentsToType("ІТ-спеціаліст", residentTypes, residents, new string[] { "Марія", "Іван" });
            ResidentType.AddResidentsToType("Вчитель", residentTypes, residents, new string[] { "Олександр" });
            ResidentType.AddResidentsToType("Медичний працівник", residentTypes, residents, new string[] { "Анна", });
            ResidentType.AddResidentsToType("Підприємець", residentTypes, residents, new string[] { "Олексій" });
            ResidentType.AddResidentsToType("Інженер", residentTypes, residents, new string[] { "Михайло", "Наталія", });
            ResidentType.AddResidentsToType("Науковець", residentTypes, residents, new string[] { });
            ResidentType.AddResidentsToType("Художник", residentTypes, residents, new string[] { });
            ResidentType.AddResidentsToType("Музикант", residentTypes, residents, new string[] { "Світлана", "Оксана" });
            ResidentType.AddResidentsToType("Письменник", residentTypes, residents, new string[] { });
            ResidentType.AddResidentsToType("Юрист", residentTypes, residents, new string[] { "Артем", });
            ResidentType.AddResidentsToType("Архітектор", residentTypes, residents, new string[] { "Петро" });
            ResidentType.AddResidentsToType("Кухар", residentTypes, residents, new string[] { "Сергій", "Ірина", "Дмитро" });
            ResidentType.AddResidentsToType("Фермер", residentTypes, residents, new string[] { });
            ResidentType.AddResidentsToType("Студент-медик", residentTypes, residents, new string[] { "Олена" });
            ResidentType.AddResidentsToType("Студент-інженер", residentTypes, residents, new string[] { });
            ResidentType.AddResidentsToType("Студент-економіст", residentTypes, residents, new string[] { "Віктор" });
            ResidentType.AddResidentsToType("Студент-філолог", residentTypes, residents, new string[] { "Катерина" });
            ResidentType.AddResidentsToType("Студент-правознавець", residentTypes, residents, new string[] { });
            ResidentType.AddResidentsToType("Студент-історик", residentTypes, residents, new string[] { });
        }

        //Вивведення зв'язків
        {
            //Console.WriteLine("Мови та люди, які спілкуються цією мовою:");
            //foreach (var language in languages)
            //{
            //    Console.WriteLine(language.LanguageId + " " + language.Name);
            //    Console.WriteLine("Список людей, які говорять на цій мові:");
            //    if (language.Speakers.Count == 0) Console.WriteLine("Немає людей, які говорять цією мовою");
            //    else foreach (var resident in language.Speakers) Console.WriteLine($"- {resident.Name}");
            //    Console.WriteLine();
            //}

            //Console.WriteLine();

            //Console.WriteLine("Міста та люди, які живуть у цих містах:");
            //foreach (var city in cities)
            //{
            //    Console.WriteLine(city.CityId + " " + city.Name);
            //    Console.WriteLine("Список людей, які живуть у цьому місті:");
            //    if (city.Residents.Count == 0) Console.WriteLine("Немає людей, які живуть у цьому місті");
            //    else foreach (var resident in city.Residents) Console.WriteLine($"- {resident.Name}");
            //    Console.WriteLine();
            //}

            //Console.WriteLine();


            //Console.WriteLine("Типи мешканців та мешканці цього типу:");
            //foreach (var residentType in residentTypes)
            //{
            //    Console.WriteLine(residentType.ResidentTypeId + " " + residentType.Name);
            //    Console.WriteLine("Мешканці цього типу:");
            //    if (residentType.Residents.Count == 0) Console.WriteLine("Немає мешканців цього типу");
            //    else foreach (var resident in residentType.Residents) Console.WriteLine($"- {resident.Name}");
            //    Console.WriteLine();
            //}
        }

        JSONmethods.ListToJSONusingSerializer(cities, "cities.json");
        JSONmethods.ListToJSONusingSerializer(languages, "languages.json");
        JSONmethods.ListToJSONusingSerializer(residentTypes, "restypes.json");
        JSONmethods.ListToJSONusingSerializer(residents, "residents.json");


        List<City> restored_cities = JSONmethods.JSONtoListUsingSerializer<City>("cities.json");
        foreach(City city in restored_cities)
        {
            Console.WriteLine($"{city.CityId}. {city.Name}");
        }

        Console.WriteLine();

        City.AddNewCitiesByConsole();
        JSONmethods.ListToJSONusingSerializer(cities, "cities.json");


        JSONrequests.DisplayCitiesSortedByFoundationYear();
        Console.WriteLine();
        JSONrequests.PrintLanguagesForSpeaker("Олександр");
        Console.WriteLine();
        JSONrequests.FindResidentById(1);
        Console.WriteLine();
        JSONrequests.PrintResidentTypesDescending();
}
}