using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages.Auth;

public class AccountModel : PageModel
{
    private readonly TicketOfficeContext context;
    
    public AccountModel(TicketOfficeContext context)
    {
        this.context = context;
    }

    // User's tickets.
    public List<Ticket> Tickets { get; set; } = null!;

    // Will be set when user confirm ticket return.
    [BindProperty(SupportsGet = true)] 
    public int ReturnTicketId { get; set; }

    // Called when GET request is sent to the page. Checks if the session is
    // valid then retrieves all user's tickets. 
    public ActionResult OnGet()
    {
        if (!ValidateSession())
            return RedirectToPage("/Auth/Login");

        Tickets = context.Ticket
                .Where(t => 
                    t.UserId == HttpContext.Session.GetInt32("UserId"))
                .Include(t => t.Route)
                .Include(t => t.Cities)
                .ToList();

        return Page();
    }

    // Called when user confirms ticket return.
    public ActionResult OnGetReturnTicket()
    {
        OnGet();

        Ticket? returnTicket = context.Ticket.Find(ReturnTicketId);

        if (returnTicket != null)
        {
            context.Remove(returnTicket);
            context.SaveChanges();
            return RedirectToPage("./Account");
        }
        
        return NotFound();
    }

    private bool ValidateSession()
    {
        return HttpContext.Session.GetInt32("UserId") != null;
    }
}