using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pubinno.Api.Data;

namespace Pubinno.Api.Controllers;

[ApiController]
[Route("v1/taps")]
public class TapsController : ControllerBase
{
    private readonly AppDbContext _db;

    public TapsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{deviceId}/summary")]
    public async Task<IActionResult> Summary(
        string deviceId,
        DateTime from,
        DateTime to)
    {
        var query = _db.Pours
            .Where(x => x.DeviceId == deviceId &&
                        x.StartedAt >= from &&
                        x.EndedAt <= to);

        var totalVolume = await query.SumAsync(x => (int?)x.VolumeMl) ?? 0;
        var totalPours = await query.CountAsync();

        var byProduct = await query
            .GroupBy(x => x.ProductId)
            .Select(g => new
            {
                productId = g.Key,
                volumeMl = g.Sum(x => x.VolumeMl),
                pours = g.Count()
            })
            .OrderByDescending(x => x.volumeMl)
            .ToListAsync();

        var byLocation = await query
            .GroupBy(x => x.LocationId)
            .Select(g => new
            {
                locationId = g.Key,
                volumeMl = g.Sum(x => x.VolumeMl),
                pours = g.Count()
            })
            .OrderByDescending(x => x.volumeMl)
            .ToListAsync();

        var result = new
        {
            deviceId,
            from,
            to,
            totalVolumeMl = totalVolume,
            totalPours,

            topProduct = byProduct.FirstOrDefault(),
            topLocation = byLocation.FirstOrDefault(),

            byProduct,
            byLocation
        };

        return Ok(result);
    }
}
