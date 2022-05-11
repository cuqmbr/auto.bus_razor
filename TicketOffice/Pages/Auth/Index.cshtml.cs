using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TicketOffice.Pages.Auth;

public class IndexModel : PageModel
{
    public ActionResult OnGet() => HttpContext.Session.GetInt32("UserId") is not null ? RedirectToPage("/Auth/Account") : RedirectToPage("/Auth/Login");
}