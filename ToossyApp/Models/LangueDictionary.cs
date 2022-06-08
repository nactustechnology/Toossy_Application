using System.Collections.Generic;

namespace ToossyApp.Models
{
    public static class LangueDictionary
    {
        public static Dictionary<string, string> getDictionary()
        {
            var _dictionary = new Dictionary<string, string>
            {
                {"fr-FR", "Français"},
                {"en-GB", "English"},
                {"de-DE", "Deutsch"},
                {"nl-NL", "Dutch"},
                {"it-IT", "Italiano"},
                {"es-ES", "Español"},
                {"pt-PT", "Portuguese"},
                {"pl-PL", "Polski"}
            };

            return _dictionary;
        }
    }
}
