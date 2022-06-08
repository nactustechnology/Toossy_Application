using Newtonsoft.Json.Linq;
using System;
using Xamarin.Forms.Maps;

namespace ToossyApp.Models
{
    public class ItineraryPin
    {
        public ItineraryPin()
        {
            Pin = new Pin();
        }

        public ItineraryPin(JObject pinObject)
        {
            if (pinObject != null)
            {
                //JObject jObject = JObject.Parse(json);

                Id_Message = (int)pinObject["clef"];
                Pin = new Pin();
                Pin.Label = (string)pinObject["titre"];
                Pin.Position = new Position((double)pinObject["latitude"], (double)pinObject["longitude"]);
            }
        }


        public int Id_Message { get; set; }
        public Pin Pin { get; set; }
    }

    public class JsonPin
    {
        public int clef { get; set; }
        public string titre { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
