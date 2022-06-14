using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TicketOffice.Models;

public class Route
{
    [Key]
    [BindRequired]
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "Номер")]
    [Range(1, 9999)]
    public int Number { get; set; }
    
    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "Ємність")]
    [Range(5, 40)]
    public int Capacity { get; set; }
    
    [Required]
    public List<RouteCity> Cities { get; set; } = null!;

    public List<Ticket>? Tickets { get; set; }
}