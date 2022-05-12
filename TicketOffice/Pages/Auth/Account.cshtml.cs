using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages.Account;

public class IndexModel : PageModel
{
    public List<Ticket> Tickets { get; set; }
    [BindProperty(SupportsGet = true)] public int ReturnTicketId { get; set; }
    
    private readonly TicketOfficeContext _context;
    private readonly ILogger<IndexModel> _logger;
    
    public IndexModel(TicketOfficeContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        if (!ValidateSession())
            return RedirectToPage("/Auth/Login");

        Tickets = _context.Ticket
                .Where(t => t.UserId == HttpContext.Session.GetInt32("UserId"))
                .Include(t => t.Route)
                .Include(t => t.Route.Cities)
                .ToList();

        return Page();
    }

    public IActionResult OnGetReturnTicket()
    {
        OnGet();
        
        _logger.Log(LogLevel.Information, $"\n\n\n\n {ReturnTicketId} \n\n\n\n");
        
        Ticket returnTicket = _context.Ticket.Find(ReturnTicketId);

        if (returnTicket is not null)
        {
            _context.Remove(returnTicket);
            _context.SaveChanges();
            return RedirectToPage("./Account");
        }
        
        return NotFound();
    }
    
    private bool ValidateSession() => HttpContext.Session.GetInt32("UserId") is not null;
}