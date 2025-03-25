public class Laboratory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public string Address { get; set; }
    public int SupervisorId { get; set; }
    //public string Supervisor { get; set; }
    public Supervisor? Supervisor { get; set; }
}
