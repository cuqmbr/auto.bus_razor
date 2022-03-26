#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages.Management.Tickets
{
    public class CreateModel : PageModel
    {
        private readonly TicketOffice.Data.TicketOfficeContext _context;

        public CreateModel(TicketOffice.Data.TicketOfficeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Number");
        ViewData["UserId"] = new SelectList(_context.User, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Ticket.Add(Ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
