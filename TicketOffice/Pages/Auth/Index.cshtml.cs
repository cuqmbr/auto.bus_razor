using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TicketOffice.Pages.Auth;

public class IndexModel : PageModel
{
    // Called when GET request is sent to the page. Determines what page
    // user will be redirected to depending on his/her authorization status.
    public ActionResult OnGet()
    {
        if (HttpContext.Session.GetInt32("UserId") != null)
        {
            return RedirectToPage("/Auth/Account");
        }

        return RedirectToPage("/Auth/Login");
    }
}