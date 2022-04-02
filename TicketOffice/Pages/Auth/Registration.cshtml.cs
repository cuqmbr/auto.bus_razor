using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages.Auth;

public class RegistrationModel : PageModel
{
    public IList<User> User { get; set; }
    [BindProperty] public string Email { get; set; }
    [BindProperty] public string Password { get; set; }
    public string emailValidation;
    public string passwordValidation;

    private readonly TicketOfficeContext _context;
    
    public RegistrationModel(TicketOfficeContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetInt32("UserId") != null)
        {
            return RedirectToPage("/Account/Index");
        }
        
        emailValidation = String.Empty;
        passwordValidation = String.Empty;
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        User = await _context.User
            .Where(u => u.Email == Email)
            .ToListAsync();
        
        if (ValidateEmail(Email, out emailValidation) && ValidatePassword(Password, out passwordValidation))
        {
            _context.User.Add(new User
            {
                Email = Email,
                Password = Password
            });
            await _context.SaveChangesAsync();
            
            
            User = await _context.User
                .Where(u => u.Email == Email)
                .ToListAsync();
            
            HttpContext.Session.SetInt32("UserId", User.First().Id);

            return RedirectToPage("/Account/Index");
        }

        return Page();
    }

    public bool ValidateEmail(string email, out string validationError)
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
        
        if (User.Any())
        {
            validationError = "E-mail уже зареєстровано";
            return false;
        }

        validationError = String.Empty;
        return true;
    }

    public bool ValidatePassword(string passowrd, out string validationError)
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