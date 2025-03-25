using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/supervisors")]
public class SupervisorController : ControllerBase
{
    private readonly LabDbContext _context;

    public SupervisorController(LabDbContext context)
    {
        _context = context;
    }

    // GET: api/supervisors
    [HttpGet]
    public async Task<IActionResult> GetSupervisors()
    {
        var supervisors = await _context.Supervisors.Include(s => s.Laboratories).ToListAsync();
        return Ok(supervisors);
    }

    // GET: api/supervisors/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSupervisor(int id)
    {
        var supervisor = await _context.Supervisors.Include(s => s.Laboratories)
                            .FirstOrDefaultAsync(s => s.Id == id);
        if (supervisor == null) return NotFound();
        return Ok(supervisor);
    }

    // POST: api/supervisors
    [HttpPost]
    public async Task<IActionResult> CreateSupervisor([FromBody] Supervisor supervisor)
    {
        _context.Supervisors.Add(supervisor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSupervisor), new { id = supervisor.Id }, supervisor);
    }

    // PUT: api/supervisors/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupervisor(int id, [FromBody] Supervisor supervisor)
    {
        if (id != supervisor.Id) return BadRequest();

        _context.Entry(supervisor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/supervisors/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupervisor(int id)
    {
        var supervisor = await _context.Supervisors.FindAsync(id);
        if (supervisor == null) return NotFound();

        _context.Supervisors.Remove(supervisor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
