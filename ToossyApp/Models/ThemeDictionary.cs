using ToossyApp.Resources;
using System.Collections.Generic;

namespace ToossyApp.Models
{
    public static class ThemeDictionary
    {
        public static Dictionary<string, string> getDictionary()
        {
            var _dictionary = new Dictionary<string, string>
            {
                {"1", AppResources.ThemeDictionary_HistoryLabel },
                {"2", AppResources.ThemeDictionary_ParkLabel },
                {"3", AppResources.ThemeDictionary_UrbanismLabel },
                {"4", AppResources.ThemeDictionary_ArchitectureLabel},
                {"5", AppResources.ThemeDictionary_LeisureLabel},
                {"6", AppResources.ThemeDictionary_StreetArt},
                {"7", AppResources.ThemeDictionary_Others}
            };

            return _dictionary;
        }
    }
}
