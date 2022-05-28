using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

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
    public ICollection<RouteCity> Cities { get; set; }
    
    public ICollection<Ticket>? Tickets { get; set; }
}