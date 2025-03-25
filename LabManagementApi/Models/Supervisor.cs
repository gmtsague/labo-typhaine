using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Supervisor
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public string Email { get; set; }
    
    public string Phone { get; set; }

    [JsonIgnore] // Prevent serialization loops
    public List<Laboratory> Laboratories { get; set; } = new();

}

