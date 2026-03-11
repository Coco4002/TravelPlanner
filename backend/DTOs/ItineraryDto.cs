namespace TravelPlanner.API.DTOs
{
    public class ItineraryDto
    {
        public int DestinationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int NumberOfDays { get; set; }
        public string Preferences { get; set; } = string.Empty; // ex: "romantic, budget-friendly, food"
    }
}