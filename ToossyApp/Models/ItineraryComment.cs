
namespace ToossyApp.Models
{
    public class ItineraryComment
    {
        public ItineraryComment()
        {
            IsTransmitted = false;
        }

        public int ItineraryKey { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public bool IsTransmitted { get; set; }
    }
}
