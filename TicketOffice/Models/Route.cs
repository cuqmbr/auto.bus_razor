using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Models;

public class Route
{
    public int Id { get; set; }
    
    public string Number { get; set; }
    public int Capacity { get; set; }

    public ICollection<City> Cities { get; set; }
    public ICollection<Ticket>? Tickets { get; set; }
}