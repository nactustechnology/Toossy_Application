using Newtonsoft.Json.Linq;
using System;

namespace ToossyApp.Models
{
    public class SelectedItem
    {
        public SelectedItem() 
        {
        }

        public SelectedItem(JObject tokenResultObject)
        {
            if (tokenResultObject != null)
            {
                Clef = (int)tokenResultObject["clef_parcours"];
                DateLimit = DateTimeOffset.FromUnixTimeMilliseconds((long)tokenResultObject["date_fin"]);
            }
        }

        public int Clef { get; set; }
        public DateTimeOffset DateLimit { get; set; }
    }
}
