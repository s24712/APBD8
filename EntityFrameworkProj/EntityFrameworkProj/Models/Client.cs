using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkProj.Models;

public class Client
{
    [Key]
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PESEL { get; set; }
    public ICollection<ClientTrip> ClientTrips { get; set; }
}