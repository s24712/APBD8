using EntityFrameworkProj.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProj.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TripsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetTrips()
    {
        var trips = await _context.Trips
            .OrderByDescending(t => t.StartDate)
            .Select(t => new TripDto
            {
                IdTrip = t.IdTrip,
                Name = t.Name,
                StartDate = t.StartDate,
                EndDate = t.EndDate
            })
            .ToListAsync();

        return Ok(trips);
    }
}

public class TripDto
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}