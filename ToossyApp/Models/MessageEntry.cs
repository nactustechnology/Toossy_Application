using Newtonsoft.Json.Linq;

namespace ToossyApp.Models
{
    public class MessageEntry
    {
        public MessageEntry()
        {
        }

        public MessageEntry(JObject messageObject)
        {
            if (messageObject != null)
            {
                //JObject jObject = JObject.Parse(json);

                Clef = (int)messageObject["clef"];
                Clef_parcours = (int)messageObject["clef_parcours"];
                Titre = (string)messageObject["titre"];
                Texte = (string)messageObject["texte"];
                Titre_illustrations = (string)messageObject["titre_illustrations"];
                Illustrations = (string)messageObject["illustrations"];
                Payant = (int)messageObject["payant"];
            }
        }


        public int Clef { get; set; }
        public int Clef_parcours { get; set; }
        public int Ordering { get; set; }
        public string Titre { get; set; }
        public string Texte { get; set; }
        public string Titre_illustrations { get; set; }
        public string Illustrations { get; set; }
        public int Payant { get; set; }
        public int ViewStatus { get; set; }
    }
}
