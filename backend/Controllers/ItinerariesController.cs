using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TravelPlanner.API.Data;
using TravelPlanner.API.DTOs;
using TravelPlanner.API.Models;

namespace TravelPlanner.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItinerariesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItinerariesController(AppDbContext context)
        {
            _context = context;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet]
        public async Task<IActionResult> GetMyItineraries()
        {
            var userId = GetUserId();
            var itineraries = await _context.Itineraries
                .Include(i => i.Destination)
                .Where(i => i.UserId == userId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();

            return Ok(itineraries);
        }

        [HttpGet("cart")]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();
            var cart = await _context.Itineraries
                .Include(i => i.Destination)
                .Where(i => i.UserId == userId && i.IsInCart)
                .ToListAsync();

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItineraryDto dto)
        {
            var destination = await _context.Destinations.FindAsync(dto.DestinationId);
            if (destination == null) return NotFound("Destinația nu există.");

            var itinerary = new Itinerary
            {
                Title = dto.Title,
                UserId = GetUserId(),
                DestinationId = dto.DestinationId,
                NumberOfDays = dto.NumberOfDays,
                Preferences = dto.Preferences,
                IsInCart = true
            };

            _context.Itineraries.Add(itinerary);
            await _context.SaveChangesAsync();

            return Ok(itinerary);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var itinerary = await _context.Itineraries.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (itinerary == null) return NotFound();

            _context.Itineraries.Remove(itinerary);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Itinerariu șters." });
        }

        [HttpPut("{id}/toggle-cart")]
        public async Task<IActionResult> ToggleCart(int id)
        {
            var userId = GetUserId();
            var itinerary = await _context.Itineraries.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (itinerary == null) return NotFound();

            itinerary.IsInCart = !itinerary.IsInCart;
            await _context.SaveChangesAsync();

            return Ok(itinerary);
        }
    }
}