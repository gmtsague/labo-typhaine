using System.ComponentModel.DataAnnotations;
public class Computer
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string MacAddress { get; set; }   
    
    public string IPAddress { get; set; }

    public string ComputerName { get; set; }

    public string RamSize { get; set; }
    public string OperatingSystem { get; set; }
    public string DiskSize { get; set; }
    public string DiskBrand { get; set; }

    public int? RoomId { get; set; }
    //  public Room? Room { get; set; }

    public DateTime SaveDate { get; set; }
}