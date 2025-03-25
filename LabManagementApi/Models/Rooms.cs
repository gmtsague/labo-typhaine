using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int Capacity { get; set; }

    public int LaboratoryId { get; set; }
    public Laboratory? Laboratory { get; set; }

    [JsonIgnore] // Prevent serialization loops
    public List<Computer>? Computers { get; set; }
}
