using System.Collections.Generic;

namespace ToossyApp.Models
{
    public static class DistanceDictionary
    {
        public static Dictionary<string, string> getDictionary()
        {
            var _dictionary = new Dictionary<string, string>
            {
                {"0.1", "100m"},
                {"0.2", "200m"},
                {"0.3", "300m"},
                {"0.4", "400m"},
                {"0.5", "500m"},
                {"1", "1km"},
                {"1.5", "1,5km"},
                {"2", "2km"},
                {"2.5", "2,5km"},
                {"3", "3km"},
                {"3.5", "3,5km"},
                {"4", "4km"},
                {"4.5", "4,5km"},
                {"5", "5km"},
                {"10", "10km"},
                {"20", "20km"},
                {"30", "30km"}
            };

            return _dictionary;
        }
    }
}
