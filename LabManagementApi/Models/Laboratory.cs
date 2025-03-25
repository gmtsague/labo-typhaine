using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Laboratory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime CreationDate { get; set; }

    public string Address { get; set; }

    public int SupervisorId { get; set; }
    public Supervisor? Supervisor { get; set; }

    [JsonIgnore] // Prevent serialization loops
    public List<Room>? Rooms { get; set; }
}
