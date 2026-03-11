using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPlanner.API.Data;
using TravelPlanner.API.Services;

namespace TravelPlanner.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AiController : ControllerBase
    {
        private readonly AiService _aiService;
        private readonly AppDbContext _context;

        public AiController(AiService aiService, AppDbContext context)
        {
            _aiService = aiService;
            _context = context;
        }

        [HttpPost("generate-itinerary")]
        public async Task<IActionResult> GenerateItinerary([FromBody] GenerateRequest request)
        {
            var destination = await _context.Destinations.FindAsync(request.DestinationId);
            if (destination == null) return NotFound("Destinația nu există.");

            var result = await _aiService.GenerateItinerary(
                destination.Name,
                destination.Country,
                request.NumberOfDays,
                request.Preferences
            );

            return Ok(new { itinerary = result });
        }
    }

    public class GenerateRequest
    {
        public int DestinationId { get; set; }
        public int NumberOfDays { get; set; }
        public string Preferences { get; set; } = string.Empty;
    }
}