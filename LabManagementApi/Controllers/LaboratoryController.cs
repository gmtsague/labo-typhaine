using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Route("api/laboratories")]
[ApiController]
public class LaboratoryController : ControllerBase
{
    private readonly LabDbContext _context;

    public LaboratoryController(LabDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetLabs()
    {
        return Ok(await _context.Laboratories.Include(l => l.Supervisor).ToListAsync());
    }

    // GET: api/laboratories/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLab(int id)
    {
        var lab = await _context.Laboratories.Include(r => r.Supervisor).Include(r => r.Rooms)
                        .FirstOrDefaultAsync(r => r.Id == id);
        if (lab == null) return NotFound();
        return Ok(lab);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLab(Laboratory lab)
    {
        _context.Laboratories.Add(lab);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetLabs), new { id = lab.Id }, lab);
    }

    // PUT: api/laboratories/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLab(int id, [FromBody] Laboratory lab)
    {
        if (id != lab.Id) return BadRequest();

        _context.Entry(lab).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/laboratories/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLab(int id)
    {
        var lab = await _context.Laboratories.FindAsync(id);
        if (lab == null) return NotFound();

        _context.Laboratories.Remove(lab);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
