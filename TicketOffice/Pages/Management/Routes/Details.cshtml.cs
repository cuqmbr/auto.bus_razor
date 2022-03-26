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
    public class DetailsModel : PageModel
    {
        private readonly TicketOffice.Data.TicketOfficeContext _context;

        public DetailsModel(TicketOffice.Data.TicketOfficeContext context)
        {
            _context = context;
        }

        public Route Route { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Route = await _context.Route.FirstOrDefaultAsync(m => m.Id == id);

            if (Route == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
