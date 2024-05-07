using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesInfo
{
    namespace ConsoleMenu
    {
        public class MenuConsole
        {
            public class Element
            {
                public string ElementName;
                public Action action;

                public Element(string elementName, Action action)
                {
                    this.ElementName = elementName;
                    this.action = action;
                }
            }

            List<object> StartMenu = new List<object>()
            {
                WriteConsoleInfo
            };
            public static List<Element> WriteConsoleInfo = new List<Element>()
            {
                new Element("Вивести міста за допомогою XmlSerializer", City.PrintAllCitiesUsingSerializer),
                new Element("Вивести міста за допомогою XmlDocument", City.PrintAllCitiesUsingDocument),
            };
        }
    }
}
