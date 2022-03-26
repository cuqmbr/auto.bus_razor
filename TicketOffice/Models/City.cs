using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Models;

public class City
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(30, ErrorMessage = "City name can't be more than 30"), MinLength(2, ErrorMessage = "City name can't be less than 2")]
    public string Name { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime? ArrivalTime { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime? DepartureTime { get; set; }
    
    [Required]
    public decimal Distance { get; set; }
    
    public int RouteId { get; set; }
    public Route Route { get; set; }
}