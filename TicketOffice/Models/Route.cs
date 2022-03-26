using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Models;

public class Route
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(4, ErrorMessage = "Route number must be 4"), MinLength(4, ErrorMessage = "Route number must be 4")]
    public string Number { get; set; }
    
    public ICollection<City>? Cities { get; set; }
    
    [Required]
    [Range(5, 50, ErrorMessage = "Capacity must be between 5 and 50")]
    public int Capacity { get; set; }
    
    public int RemainingCapacity { get; set; }
    
    public ICollection<Ticket>? Tickets { get; set; }
}