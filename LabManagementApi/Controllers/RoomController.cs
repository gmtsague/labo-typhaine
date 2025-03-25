using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/rooms")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly LabDbContext _context;

    public RoomController(LabDbContext context)
    {
        _context = context;
    }

    /*[HttpPost]
    public async Task<IActionResult> CreateRoom(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateRoom), new { id = room.Id }, room);
    }
    */
    // GET: api/rooms
    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _context.Rooms.Include(r => r.Laboratory).Include(r => r.Computers).ToListAsync();
        return Ok(rooms);
    }

    // GET: api/rooms/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoom(int id)
    {
        var room = await _context.Rooms.Include(r => r.Laboratory).Include(r => r.Computers)
                        .FirstOrDefaultAsync(r => r.Id == id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    // POST: api/rooms
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    // PUT: api/rooms/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room room)
    {
        if (id != room.Id) return BadRequest();

        _context.Entry(room).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/rooms/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null) return NotFound();

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
