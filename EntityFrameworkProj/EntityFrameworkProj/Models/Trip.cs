namespace EntityFrameworkProj.Models;

public class Trip
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<ClientTrip> ClientTrips { get; set; }
}