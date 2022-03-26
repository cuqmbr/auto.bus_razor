using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int RouteId { get; set; }
    public Route Route { get; set; }
}