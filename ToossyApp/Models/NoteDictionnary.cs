using System.Collections.Generic;

namespace ToossyApp.Models
{
    public static class NoteDictionnary
    {
        public static Dictionary<string, string> getDictionary()
        {
            var _dictionary = new Dictionary<string, string>
            {
                {"0","0/5"},
                {"1","1/5"},
                {"2","2/5"},
                {"3","3/5"},
                {"4","4/5"},
                {"5","5/5"}
            };

            return _dictionary;
        }
    }
}
