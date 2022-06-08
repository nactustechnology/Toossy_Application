
namespace ToossyApp.Models
{
    public class ItineraryReport
    {
        public ItineraryReport()
        {
            IsTransmitted = false;
        }

        public int ItineraryKey { get; set; }
        public string Comment { get; set; }
        public bool IsTransmitted { get; set; }
    }
}
