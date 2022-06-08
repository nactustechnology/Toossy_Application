using System.Collections.Generic;
using System.Globalization;


namespace ToossyApp.Models
{
    public static class FiltreDictionary
    {
        public static Dictionary<string, string[]> getDictionary()
        {
            var languagesDictionnary = LangueDictionary.getDictionary();
            string language = default(string);

            if ( languagesDictionnary.ContainsKey(CultureInfo.CurrentCulture.Name))
            {
                language = CultureInfo.CurrentCulture.Name;
                
            }
            else
            {
                language = findLanguage(CultureInfo.CurrentCulture.Name);
            }

            var _dictionary = new Dictionary<string, string[]>
            {
                {"FiltreTheme" , new string[] { "1", "2", "3", "4", "5", "6", "7" }},
                {"FiltreDistance" , new string[] { "30" }},
                {"FiltreDuree" , new string[] { "240" }},
                {"FiltreTypeParcours" , new string[] { "0", "1" }},
                {"FiltreNote" , new string[] { "0" }},
                {"FiltreGratuitTarif" , new string[] { "0" }},
                {"FiltreLangue" , new string[] { language }},
            };


            return _dictionary;
        }

        static string findLanguage(string currentCultureLanguage)
        {
            var researchedLanguage = currentCultureLanguage.Substring(0, 2);

            var languagesKeys = LangueDictionary.getDictionary().Keys;

            string result = default(string);

            foreach ( string lang in languagesKeys)
            {
                if(lang.StartsWith(researchedLanguage))
                    result = lang;
            }

            if (result == default(string))
                result = "en-GB";

            return result;
        }
    }
}
