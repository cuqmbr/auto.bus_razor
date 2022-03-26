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

namespace TicketOffice.Pages.Management.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly TicketOffice.Data.TicketOfficeContext _context;

        public IndexModel(TicketOffice.Data.TicketOfficeContext context)
        {
            _context = context;
        }

        public IList<Ticket> Ticket { get;set; }

        public async Task OnGetAsync()
        {
            Ticket = await _context.Ticket
                .Include(t => t.Route)
                .Include(t => t.User).ToListAsync();
        }
    }
}
