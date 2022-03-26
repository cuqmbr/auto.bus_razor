#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;
using Route = TicketOffice.Models.Route;

namespace TicketOffice.Pages.Management.Routes
{
    public class IndexModel : PageModel
    {
        private readonly TicketOffice.Data.TicketOfficeContext _context;

        public IndexModel(TicketOffice.Data.TicketOfficeContext context)
        {
            _context = context;
        }

        public IList<Route> Route { get;set; }

        public async Task OnGetAsync()
        {
            Route = await _context.Route.ToListAsync();
        }
    }
}
