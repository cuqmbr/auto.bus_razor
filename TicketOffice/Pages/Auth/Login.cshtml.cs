using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages.Auth;

public class LoginModel : PageModel
{
    [BindProperty] public User? User { get; set; }
    
    public string EmailValidationError;
    public string PasswordValidationError;

    private readonly TicketOfficeContext _context;
    
    public LoginModel(TicketOfficeContext context)
    {
        _context = context;
    }

    public ActionResult OnGet() => ValidateSession() ? RedirectToPage("/Auth/Account") : Page();

    public ActionResult OnPost()
    {
        if (ValidateForm())
        {
            User user = _context.User.FirstOrDefault(u => u.Email == User.Email);
            
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetInt32("IsManager", user.IsManager ? 1 : 0);
            return RedirectToPage("/Auth/Account");
        }

        return Page();
    }

    private bool ValidateForm()
    {
        User? user = _context.User.FirstOrDefault(u => u.Email == User.Email);

        return ValidateEmail(User.Email, out EmailValidationError) && ValidatePassword(User.Password, out PasswordValidationError);
        
        bool ValidateEmail(string email, out string validationError)
        {
            if (user is not null)
            {
                validationError = String.Empty;
                return true;
            }
        
            if (String.IsNullOrWhiteSpace(email))
            {
                validationError = "Поле має бути заповненим";
                return false;
            }
        
            Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        
            if (!emailRegex.IsMatch(email))
            {
                validationError = "E-mail некоректний";
                return false;
            }

            validationError = "E-mail не зареєстровано";
            return false;
        }

        bool ValidatePassword(string password, out string validationError)
        {
            if (user.Password == password)
            {
                validationError = String.Empty;
                return true;
            }
        
            if (String.IsNullOrWhiteSpace(password))
            {
                validationError = "Поле має бути заповненим";
                return false;
            }

            validationError = "Неправильний пароль";
            return false;
        }
    }
    
    private bool ValidateSession() => HttpContext.Session.GetInt32("UserId") is not null;
}