using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pubinno.Api.Data;

[ApiController]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _db;

    public HealthController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("/health")]
    public async Task<IActionResult> Get()
    {
        var canConnect = await _db.Database.CanConnectAsync();

        if (!canConnect)
            return StatusCode(503, new { status = "fail", db = "fail" });

        return Ok(new { status = "ok", db = "ok" });
    }
}
