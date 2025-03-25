using System.Text.Json.Serialization;

public class Supervisor
{
    public int Id { get; set; }
    public string FullName { get; set; }
    //public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    [JsonIgnore] // Prevent serialization loops
    public List<Laboratory> Laboratories { get; set; } = new();
}