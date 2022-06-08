using Xamarin.Forms.Maps;

namespace ToossyApp.Models
{
    public class MessagePin
    {
        public int Id_Itinerary { get; set; }
        public int Id_Message { get; set; }
        public int Id_Next_Message { get; set; }
        public Pin Pin { get; set; }
        public int ViewStatus { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsTelechargeable { get; set; }
    }
}
