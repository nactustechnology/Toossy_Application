using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ToossyApp.Models
{
    public class ItineraryEntry
    {
        public ItineraryEntry()
        { }

        public ItineraryEntry(JObject itineraryObject)
        {
            if (itineraryObject != null)
            {
                //JObject jObject = JObject.Parse(json);

                Clef = (int)itineraryObject["clef"];
                Titre = (string)itineraryObject["titre"];
                Description = (string)itineraryObject["description"];
                Titre_illustrations = (string)itineraryObject["titre_illustrations"];
                Illustrations = (string)itineraryObject["illustrations"];
                Type_parcours = (int)itineraryObject["type_parcours"];
                Theme = (int)itineraryObject["theme"];
                Langue = (string)itineraryObject["langue"];
                Duree = (int)itineraryObject["duree"];
                Payant = (int)itineraryObject["payant"];
                Telechargeable = (int)itineraryObject["telechargeable"];
                Tarif = (double)itineraryObject["tarif"];
                Tva = (double)itineraryObject["tva"];
                Currency = (string)itineraryObject["currency"];
                Nombre_message = (int)itineraryObject["nombre_messages"];
                Note = (double)itineraryObject["note"];
                Nombre_commentaires = (int)itineraryObject["nombre_commentaires"];
                Latitude = (double)itineraryObject["latitude"];
                Longitude = (double)itineraryObject["longitude"];

                var _itinerarypins = (string)itineraryObject["itinerarypins"];
                var _itinerarypins_UrlDecode = System.Net.WebUtility.UrlDecode(_itinerarypins);
                var _itinerarypins_DeserializeObject = JsonConvert.DeserializeObject<object[]>(_itinerarypins_UrlDecode);

                if(_itinerarypins_DeserializeObject!=null)
                {
                    List<ItineraryPin> ItineraryPinsList = new List<ItineraryPin>();

                    foreach (JObject pinArray in _itinerarypins_DeserializeObject)
                    {
                        ItineraryPinsList.Add(new ItineraryPin(pinArray));
                    }

                    ItineraryPins = ItineraryPinsList.ToArray();
                }
                    

                IsSelected = false;
            }
        }


        public int Clef { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Titre_illustrations { get; set; }
        public string Illustrations { get; set; }
        public int Type_parcours { get; set; }
        public int Theme { get; set; }
        public string Langue { get; set; }
        public int Duree { get; set; }
        public int Payant { get; set; }
        public int Telechargeable { get; set; }
        public double Tarif { get; set; }
        public double Tva { get; set; }
        public string Currency { get; set; }
        public int Nombre_message { get; set; }
        public double Note { get; set; }
        public int Nombre_commentaires { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ItineraryPin[] ItineraryPins { get; set; }
        public bool IsSelected { get; set; }
    }
}
