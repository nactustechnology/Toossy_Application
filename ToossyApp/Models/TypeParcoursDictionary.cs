using ToossyApp.Resources;
using System.Collections.Generic;

namespace ToossyApp.Models
{
    public static class TypeParcoursDictionary
    {
        public static Dictionary<string, string> getDictionary()
        {
            var _dictionary = new Dictionary<string, string>
            {
                {"0", AppResources.TypeParcoursDictionary_WalkLabel },
                {"1", AppResources.TypeParcoursDictionary_TourLabel}
            };

            return _dictionary;
        }
    }
}
