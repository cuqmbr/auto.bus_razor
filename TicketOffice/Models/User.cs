using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace TicketOffice.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(24, ErrorMessage = "Ім'я не може бути більше 24 символів"),
     MinLength(4, ErrorMessage = "Ім'я не може бути менше 4 символів")]
    [Display(Name = "Ім'я")]
    public string? FirstName { get; set; }
    
    [MaxLength(24, ErrorMessage = "Прізвище не може бути більше 24 символів"),
     MinLength(4, ErrorMessage = "Прізвище не може бути менше 4 символів")]
    [Display(Name = "Прізвище")]
    public string? LastName { get; set; }
    
    [MaxLength(24, ErrorMessage = "Ім'я по батькові не може бути більше 24 символів"),
     MinLength(4, ErrorMessage = "Ім'я по батькові не може бути менше 4 символів")]
    [Display(Name = "По батькові")]
    public string? Patronymic { get; set; }
    
    
    [MaxLength(48, ErrorMessage = "E-mail не може бути більше 48 символів"),
     MinLength(6, ErrorMessage = "E-mail не може бути менше 6 символів")]
    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "E-mail")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
     ErrorMessage = "E-mail невалідний")]
    public string Email { get; set; }
    
    [MaxLength(32, ErrorMessage = "Пароль не може бути більше 32 символів"),
     MinLength(8, ErrorMessage = "Пороль не може бути менше 8 символів")]
    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
     ErrorMessage = "Пароль має містити 1 маленьку та велику літери, 1 цифру, 1 спеціальний символ (#, $, @ та ін.), бути написаний латиницею та мати більше 8 символів")]
    public string Password { get; set; }

    
    public ICollection<Ticket>? Tickets { get; set; }
    
    [Required]
    public bool IsManager { get; set; }
}