using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages.Auth;

public class RegistrationModel : PageModel
{
    [BindProperty] public User User { get; set; }
    
    public string EmailValidationError;
    public string PasswordValidationError;

    private readonly TicketOfficeContext _context;
    
    public RegistrationModel(TicketOfficeContext context)
    {
        _context = context;
    }

    public ActionResult OnGet() => ValidateSession() ? RedirectToPage("/Auth/Account") : Page();

    public ActionResult OnPost()
    {
        if (ValidateForm())
        {
            _context.User.Add(User);
            _context.SaveChanges();

            User = _context.User.FirstOrDefault(u => u.Email == User.Email);
            
            HttpContext.Session.SetInt32("UserId", User.Id);

            return RedirectToPage("/Auth/Account");
        }
            
        return Page();
    }

    private bool ValidateForm()
    {
        return ValidateEmail(User.Email, out EmailValidationError) && ValidatePassword(User.Password, out PasswordValidationError);

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

            User user = _context.User.FirstOrDefault(u => u.Email == User.Email);

            if (user is not null)
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