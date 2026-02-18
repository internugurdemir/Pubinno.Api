using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pubinno.Api.Constants;
using Pubinno.Api.Data;
using Pubinno.Api.Entities;
using Pubinno.Api.Models;

namespace Pubinno.Api.Controllers;

[ApiController]
[Route("v1/pours")]
public class PoursController : ControllerBase
{
    private readonly AppDbContext _db;

    public PoursController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create(PourRequest request)
    {
        // Validation
        if (!AllowedValues.Products.Contains(request.ProductId) ||
            !AllowedValues.Locations.Contains(request.LocationId) ||
            !AllowedValues.Volumes.Contains(request.VolumeMl) ||
            request.EndedAt < request.StartedAt)
        {
            return BadRequest();
        }

        var entity = new PourEvent
        {
            EventId = request.EventId,
            DeviceId = request.DeviceId,
            LocationId = request.LocationId,
            ProductId = request.ProductId,
            StartedAt = request.StartedAt,
            EndedAt = request.EndedAt,
            VolumeMl = request.VolumeMl
        };

        try
        {
            _db.Pours.Add(entity);
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            // duplicate eventId → idempotent
            return Ok();
        }

        return Ok();
    }
}
