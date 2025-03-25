using System.Text.Json.Serialization;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    // Foreign Key to Laboratory
    public int LaboratoryId { get; set; }
    public Laboratory? Laboratory { get; set; }
    
    [JsonIgnore] // Prevent serialization loops
    public List<Computer>? Computers { get; set; }
}

/*
public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }

    // Foreign Key to Laboratory
    public int LaboratoryId { get; set; }
    public Laboratory Laboratory { get; set; }

    // List of Computers in this Room
    public List<Computer> Computers { get; set; } = new();
}
*/