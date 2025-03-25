using Microsoft.EntityFrameworkCore;
//using LabManagementApi.Models;

public class LabDbContext : DbContext
{
    public LabDbContext(DbContextOptions<LabDbContext> options) : base(options) { }

    public DbSet<Laboratory> Laboratories { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Computer> Computers { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Computer>()
            .Property(u => u.SaveDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
