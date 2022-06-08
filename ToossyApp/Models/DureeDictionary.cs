using ToossyApp.Resources;
using System.Collections.Generic;

namespace ToossyApp.Models
{
    public static class DureeDictionary
    {
        public static Dictionary<string, string> getDictionary()
        {
            var _dictionary = new Dictionary<string, string>
            {
                {"0",AppResources.DureeDictionary_FreeLabel },
                {"30", "30 min."},
                {"60", "1h"},
                {"90", "1h30"},
                {"120", "2h"},
                {"150", "2h30"},
                {"180", "3h"},
                {"210", "3h30"},
                {"240", "4h"}
            };

            return _dictionary;
        }
    }
}
