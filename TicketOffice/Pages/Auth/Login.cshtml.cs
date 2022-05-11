using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages.Auth;

public class LoginModel : PageModel
{
    [BindProperty] public string Email { get; set; } = String.Empty;
    [BindProperty] public string Password { get; set; } = String.Empty;
    
    public string EmailValidation;
    public string PasswordValidation;
    
    private List<User> User { get; set; }

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
            HttpContext.Session.SetInt32("UserId", User.First().Id);
            return RedirectToPage("/Auth/Account");
        }

        return Page();
    }

    private bool ValidateForm()
    {
        User = _context.User
            .Where(u => u.Email == Email)
            .ToList();

        return ValidateEmail(Email, out EmailValidation) && ValidatePassword(Password, out PasswordValidation);
        
        bool ValidateEmail(string email, out string validationError)
        {
            if (User.Count == 1)
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
            if (User.First().Password == password)
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