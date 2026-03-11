namespace TravelPlanner.API.Models
{
    public class Itinerary
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public int DestinationId { get; set; }
        public Destination Destination { get; set; } = null!;
        public int NumberOfDays { get; set; }
        public string GeneratedPlan { get; set; } = string.Empty; // raspunsul AI cached
        public string Preferences { get; set; } = string.Empty; // ce a cerut userul
        public bool IsInCart { get; set; } = true; // tinut in "cos" intre sesiuni
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}