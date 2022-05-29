using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Models;

public class Route
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "Номер")]
    [Range(1, 256)]
    public int Number { get; set; }
    
    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "Ємність")]
    [Range(5, 40)]
    public int Capacity { get; set; }
    
    [Required]
    public ICollection<RouteCity> Cities { get; set; } = null!;

    public ICollection<Ticket>? Tickets { get; set; }
}