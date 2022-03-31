using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages.Auth;

public class LoginModel : PageModel
{
    private readonly TicketOfficeContext _context;
    
    public LoginModel(TicketOfficeContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public User User { get; set; }
    
    public async Task<IActionResult> OnPostAsync()
    {
        //Login logic
        return Page();
    }
}