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

namespace TicketOffice.Pages.Management.Cities
{
    public class IndexModel : PageModel
    {
        private readonly TicketOffice.Data.TicketOfficeContext _context;

        public IndexModel(TicketOffice.Data.TicketOfficeContext context)
        {
            _context = context;
        }

        public IList<City> City { get;set; }

        public async Task OnGetAsync()
        {
            City = await _context.City
                .Include(c => c.Route).ToListAsync();
        }
    }
}
