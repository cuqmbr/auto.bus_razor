using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(16, ErrorMessage = "First name lenght can't be more than 16"), MinLength(2, ErrorMessage = "First name can't be less than 2")]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(16, ErrorMessage = "Last name lenght can't be more than 16"), MinLength(2, ErrorMessage = "Last name can't be less than 2")]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(16, ErrorMessage = "Patronymic lenght can't be more than 16"), MinLength(2, ErrorMessage = "Patronymic can't be less than 2")]
    public string Patronymic { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [StringLength(64, ErrorMessage = "Password lenght can't be more than 64"), MinLength(8, ErrorMessage = "Password lenght can't be less than 8")]
    //[RegularExpression()]
    public string Password { get; set; }
    
    public ICollection<Ticket>? Tickets { get; set; }

    [Required] 
    public bool IsManager { get; set; } = false;
}