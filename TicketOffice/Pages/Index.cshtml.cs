using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;
using Route = TicketOffice.Models.Route;

namespace TicketOffice.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly TicketOfficeContext _context;

    public IndexModel(ILogger<IndexModel> logger, TicketOfficeContext context)
    {
        _logger = logger;
        _context = context;
    }

    public List<Route> Route { get; set; } = null!;
    [BindProperty(SupportsGet = true)] public string from { get; set; }
    [BindProperty(SupportsGet = true)] public string to { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime date { get; set; } = new DateTime(2022, 03, 28, 0, 0, 0).Date;

    public void OnGet()
    {
        Route = _context.Route
            .Include(r => r.Cities)
            .Include(r => r.Tickets)
            .ToList();

        if (!string.IsNullOrWhiteSpace(from) && !string.IsNullOrWhiteSpace(to))
        {
            FilterRoutes();
        }
    }

    public void OnGetSortByDuration(bool isDescending)
    {
        OnGet();

        Route.Sort((x, y) => (isDescending ? -1 : 1) * 
            (x.Cities.Last().ArrivalTime - x.Cities.First().DepartureTime).Value
            .CompareTo((y.Cities.Last().ArrivalTime - y.Cities.First().DepartureTime).Value) + 1);
    }


private void FilterRoutes()
    {
        Route.ForEach(r => r.Cities = r.Cities
            .SkipWhile(c => c.Name != from).Reverse()
            .SkipWhile(c => c.Name != to).Reverse()
            .ToList());

        Route.RemoveAll(r => 
            r.Cities.Count < 2 || r.Cities.First().DepartureTime.Value.DayOfYear != date.DayOfYear);
    }
}