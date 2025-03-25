/*public class Computer
{
    public int Id { get; set; }
    public string IPAddress { get; set; }
    public string MACAddress { get; set; }
    public int RAM { get; set; }
    public string OS { get; set; }
    public int DiskSize { get; set; }
    public string DiskBrand { get; set; }
    public int RoomId { get; set; }
}
*/
public class Computer
{
    public int Id { get; set; }
    public string ComputerName { get; set; }
    public string MacAddress { get; set; }
    public string IpAddress { get; set; }
    public string RamSize { get; set; } // In GB
    public string DiskSize { get; set; } // In GB
    public string OperatingSystem { get; set; }
    public string DiskBrand { get; set; }
    
    // Foreign Key to Room
    public int? RoomId { get; set; }
    public Room? Room { get; set; }

    public DateTime? SaveDate { get; set; }
}