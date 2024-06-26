using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkProj.Models;

public class ClientTrip
{
    [Key]
    public int IdClient { get; set; }
    public Client Client { get; set; }
    public int IdTrip { get; set; }
    public Trip Trip { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime RegisteredAt { get; set; }
}
