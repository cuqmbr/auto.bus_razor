using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages.Auth;

public class LoginModel : PageModel
{
    public IList<User> User { get; set; }
    [BindProperty] public string Email { get; set; }
    [BindProperty] public string Password { get; set; }
    public string emailValidation;
    public string passwordValidation;

    private readonly TicketOfficeContext _context;
    
    public LoginModel(TicketOfficeContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetInt32("UserId") != null)
        {
            return RedirectToPage("/Account/Index");
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        emailValidation = String.Empty;
        passwordValidation = String.Empty;

        User = await _context.User
            .Where(u => u.Email == Email)
            .ToListAsync();

        if (ValidateEmail(Email, out emailValidation) && ValidatePassword(Password, out passwordValidation))
        {
            HttpContext.Session.SetInt32("UserId", User.First().Id);

            return RedirectToPage("Account/Index");
        }

        return Page();
    }

    public bool ValidateEmail(string email, out string validationError)
    {
        if (User.Any(u => u.Email == email))
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

    public bool ValidatePassword(string password, out string validationError)
    {
        if (User.Where(u => u.Email == Email).Any(u => u.Password == password))
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