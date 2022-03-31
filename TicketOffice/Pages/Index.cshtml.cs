using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages;

public class IndexModel : PageModel
{
    public IndexModel(TicketOfficeContext context)
    {
    }

    public void OnGet()
    {
    }
}