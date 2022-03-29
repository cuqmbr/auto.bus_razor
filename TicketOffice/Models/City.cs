using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Models;

public class City
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public DateTime? DepartureTime { get; set; }

    public int RouteId { get; set; }
    public Route Route { get; set; }
}