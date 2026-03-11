using Microsoft.AspNetCore.Identity;

namespace TravelPlanner.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
    }
}