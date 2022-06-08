namespace ToossyApp.Models
{
    public class ItineraryCharge
    {
        public int itineraryId { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public bool RememberData { get; set; }
        public bool CgvApproval { get; set; }
    }
}
