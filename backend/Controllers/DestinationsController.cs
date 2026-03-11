using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPlanner.API.Data;
using TravelPlanner.API.DTOs;
using TravelPlanner.API.Models;

namespace TravelPlanner.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DestinationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? category, [FromQuery] double? maxPrice, [FromQuery] double? minRating)
        {
            var query = _context.Destinations.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(d => d.Category == category);

            if (maxPrice.HasValue)
                query = query.Where(d => d.AveragePrice <= maxPrice.Value);

            if (minRating.HasValue)
                query = query.Where(d => d.Rating >= minRating.Value);

            var destinations = await query.OrderByDescending(d => d.Rating).ToListAsync();
            return Ok(destinations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null) return NotFound();
            return Ok(destination);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(DestinationDto dto)
        {
            var destination = new Destination
            {
                Name = dto.Name,
                Country = dto.Country,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                Category = dto.Category,
                AveragePrice = dto.AveragePrice,
                Rating = dto.Rating
            };

            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = destination.Id }, destination);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, DestinationDto dto)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null) return NotFound();

            destination.Name = dto.Name;
            destination.Country = dto.Country;
            destination.Description = dto.Description;
            destination.ImageUrl = dto.ImageUrl;
            destination.Category = dto.Category;
            destination.AveragePrice = dto.AveragePrice;
            destination.Rating = dto.Rating;

            await _context.SaveChangesAsync();
            return Ok(destination);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null) return NotFound();

            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Destinație ștearsă." });
        }
    }
}