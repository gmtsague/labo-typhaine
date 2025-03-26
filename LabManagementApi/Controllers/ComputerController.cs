using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

[Route("api/computers")]
[ApiController]
public class ComputerController : ControllerBase
{
    private readonly LabDbContext _context;
    private readonly IMemoryCache _cache;
    public ComputerController(LabDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterComputer(Computer computer)
    {
        try
        {
        _context.Computers.Add(computer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(RegisterComputer), new { id = computer.Id }, computer);
        }
        catch(Exception err)
        {
            Console.WriteLine(@"An error occured; see details: {0}", err.Message);
            return UnprocessableEntity(err.InnerException?.Message ?? err.Message);  //HttpStatusCode.UnprocessableContent
        }
    }

    [HttpPut("{id}/assign-room/{roomId}")]
    public async Task<IActionResult> AssignRoom(int id, int roomId)
    {
        var computer = await _context.Computers.FindAsync(id);
        if (computer == null) return NotFound();

        computer.RoomId = roomId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetComputers()
    {
        return Ok(await _context.Computers.Include(r => r.Room).ToListAsync());
    }

    // GET: api/computers/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetComputer(int id)
    {
        var computer = await _context.Computers.Include(r => r.Room)
                        .FirstOrDefaultAsync(r => r.Id == id);
        if (computer == null) return NotFound();
        return Ok(computer);
    }

    // PUT: api/computers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLab(int id, [FromBody] Computer computer)
    {
        if (id != computer.Id) return BadRequest();

        _context.Entry(computer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/computers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLab(int id)
    {
        var computer = await _context.Laboratories.FindAsync(id);
        if (computer == null) return NotFound();

        _context.Laboratories.Remove(computer);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("status/room/{roomId}")]
    public async Task<IActionResult> GetComputersStatus(int roomId)
    {
        // string cacheKey = $"RoomStatus_{roomId}";

        // if (_cache.TryGetValue(cacheKey, out List<ComputerStatusDto> cachedStatuses))
        // {
        //     return Ok(cachedStatuses);
        // }

        var computers = await _context.Computers
            .Where(c => c.RoomId == roomId)
            .ToListAsync();

        if (computers.Count == 0)
            return NotFound($"No computers found in room {roomId}");

        var computerStatuses = computers.Select(computer => new ComputerStatusDto
        {
            ComputerId = computer.Id,
            IPAddress = computer.IPAddress,
            IsOnline = PingHelper.IsComputerOnline(computer.IPAddress)
        }).ToList();

    //    _cache.Set(cacheKey, computerStatuses, TimeSpan.FromMinutes(5));

        return Ok(computerStatuses);
    }
}
