namespace TravelPlanner.API.DTOs
{
    public class DestinationDto
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double AveragePrice { get; set; }
        public double Rating { get; set; }
    }
}