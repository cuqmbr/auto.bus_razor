using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [MaxLength(48, ErrorMessage = "E-mail не може бути більше 48 символів"),
     MinLength(6, ErrorMessage = "E-mail не може бути менше 6 символів")]
    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "E-mail")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
     ErrorMessage = "E-mail невалідний")]
    public string Email { get; set; } = null!;

    [MaxLength(32, ErrorMessage = "Пароль має бути менше 32 символів"),
     MinLength(8, ErrorMessage = "Пороль має бути більше 8 символів")]
    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
     ErrorMessage = "Проль має містити великі та малі латинські літери, цифри та спеціальні знаки (@, $, % та ін.)")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Поле має бути заповненим")]
    [Display(Name = "Адмімістратор?")]
    public bool IsManager { get; set; }
    

    public List<Ticket>? Tickets { get; set; }
}