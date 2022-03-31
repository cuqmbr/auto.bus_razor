﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketOffice.Data;
using TicketOffice.Models;

namespace TicketOffice.Pages;

public class IndexModel : PageModel
{
    private readonly TicketOfficeContext _context;
    
    public IndexModel(TicketOfficeContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public User User { get; set; }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.User.Add(User);
        await _context.SaveChangesAsync();
        
        return RedirectToPage("./Routes");
    }
}