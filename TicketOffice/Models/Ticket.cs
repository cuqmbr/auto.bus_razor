using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketOffice.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "Ім'я пасажира")]
    public string PassengerFirstName { get; set; }
    
    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "Прізвище пасажира")]
    public string PassengerLastName { get; set; }
    
    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "Номер місця пасажира")]
    public int PassengerPlace { get; set; }
    
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [ForeignKey("Route")]
    public int RouteId { get; set; }
    public Route Route { get; set; }
}