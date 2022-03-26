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
    public class DeleteModel : PageModel
    {
        private readonly TicketOffice.Data.TicketOfficeContext _context;

        public DeleteModel(TicketOffice.Data.TicketOfficeContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Route = await _context.Route.FindAsync(id);

            if (Route != null)
            {
                _context.Route.Remove(Route);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
