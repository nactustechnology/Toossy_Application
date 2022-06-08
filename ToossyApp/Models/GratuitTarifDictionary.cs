using ToossyApp.Resources;
using System.Collections.Generic;

namespace ToossyApp.Models
{
    public static class GratuitTarifDictionary
    {
        public static Dictionary<string, string> getDictionary()
        {
            var _dictionary = new Dictionary<string, string>
            {
                {"0", AppResources.GratuitTarifDictionary_FreeLabel},
                {"1000000", AppResources.GratuitTarifDictionary_AllLabel}
            };

            /*var _dictionary = new Dictionary<string, string>
            {
                {"0", AppResources.GratuitTarifDictionary_FreeLabel},
                {"1", "<1"},
                {"2", "<2"},
                {"5", "<5"},
                {"9", "<9"},
                {"15", "<15"},
                {"1000000", AppResources.GratuitTarifDictionary_AllLabel}
            };*/

            return _dictionary;
        }
    }
}
