using EntityFrameworkProj.Context;
using EntityFrameworkProj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProj.Controllers;

public class ClientsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ClientsController(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client == null)
        {
            return NotFound();
        }

        if (client.ClientTrips.Any())
        {
            return BadRequest("Client has assigned trips.");
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("/api/trips/{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientDto assignClientDto)
    {
        var trip = await _context.Trips.FindAsync(idTrip);
        if (trip == null)
        {
            return NotFound("Trip not found.");
        }

        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.PESEL == assignClientDto.PESEL);

        if (client == null)
        {
            client = new Client
            {
                FirstName = assignClientDto.FirstName,
                LastName = assignClientDto.LastName,
                PESEL = assignClientDto.PESEL
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        if (client.ClientTrips.Any(ct => ct.IdTrip == idTrip))
        {
            return BadRequest("Client is already assigned to this trip.");
        }

        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = idTrip,
            PaymentDate = assignClientDto.PaymentDate,
            RegisteredAt = DateTime.Now
        };

        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync();

        return Ok();
    }
}

public class AssignClientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PESEL { get; set; }
    public DateTime? PaymentDate { get; set; }
}