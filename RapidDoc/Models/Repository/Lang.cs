using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidDoc.Models.Repository
{
    public struct Lang
    {
        public static List<string[]> langList = new List<string[]>() { 
            new string[] { "ru-RU", "Русский" }, 
            new string[] { "en-US", "English" }, 
            new string[] { "kk-KZ", "Қазақша" } 
        };

        public static List<string> GetISOCodes()
        {
            List<string> LangISO = new List<string>();
            foreach(string[] item in langList)
            {
                LangISO.Add(item[0]);
            }

            return LangISO;
        }

        public static string DefaultLang()
        {
            return langList[0][0];
        }
    }
}