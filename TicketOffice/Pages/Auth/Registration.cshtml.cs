using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages.Auth;

public class RegistrationModel : PageModel
{
    [BindProperty] public string Email { get; set; } = String.Empty;
    [BindProperty] public string Password { get; set; } = String.Empty;
    
    public string EmailValidation;
    public string PasswordValidation;
    
    private List<User> User { get; set; }

    private readonly TicketOfficeContext _context;
    
    public RegistrationModel(TicketOfficeContext context)
    {
        _context = context;
    }

    public ActionResult OnGet() => ValidateSession() ? RedirectToPage("/Auth/Account") : Page();

    public ActionResult OnPostAsync()
    {
        if (ValidateForm())
        {
            _context.User.Add(new User {Email = Email, Password = Password});
            
            _context.SaveChanges();
            
            User = _context.User
                .Where(u => u.Email == Email)
                .ToList();
            
            HttpContext.Session.SetInt32("UserId", User.First().Id);

            return RedirectToPage("/Auth/Account");
        }
            
        return Page();
    }

    private bool ValidateForm()
    {
        return ValidateEmail(Email, out EmailValidation) && ValidatePassword(Password, out PasswordValidation);

        bool ValidateEmail(string email, out string validationError)
        {
            Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                
            if (String.IsNullOrWhiteSpace(email))
            {
                validationError = "Поле має бути заповненим";
                return false;
            }
                
            if (!emailRegex.IsMatch(email))
            {
                validationError = "E-mail некоректний";
                return false;
            }
                
            User = _context.User
                .Where(u => u.Email == Email)
                .ToList();
                
            if (User.Any())
            {
                validationError = "E-mail уже зареєстровано";
                return false;
            }
        
            validationError = String.Empty;
            return true;
        }
        
        bool ValidatePassword(string passowrd, out string validationError)
        {
            if (String.IsNullOrWhiteSpace(passowrd))
            {
                validationError = "Поле має бути заповненим";
                return false;
            }
                
            if (passowrd.Length < 8 || passowrd.Length > 32)
            { 
                validationError = "Паороль має бути від 8 до 32 символів"; 
                return false;
            }
                
            Regex passwordRegex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
                
            if (!passwordRegex.IsMatch(passowrd))
            {
                validationError = "Пароль має містити великі та малі латинські літери, цифри та спеціальні знаки (@, $, % та ін.)";
                return false;
            }
        
            validationError = String.Empty;
            return true;
        }
    }
    
    bool ValidateSession() => HttpContext.Session.GetInt32("UserId") is not null;
}