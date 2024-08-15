namespace HejCamping.Models;

public class Booking
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public int CabinNr { get; set; }
}