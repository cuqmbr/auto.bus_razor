using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Newtonsoft.Json;
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
    [BindProperty(SupportsGet = true)] public string SortString { get; set; }

    public void OnGet()
    {
        RetrieveAllRoutes();

        if (!string.IsNullOrWhiteSpace(from))
        {
            FilterRoutesByFrom();
        }

        if (!string.IsNullOrWhiteSpace(to))
        {
            FilterRoutesByTo();
        }

        FilterRoutesByDate();
    }

    public void OnGetSortByNumber()
    {
        OnGet();

        Route.Sort((x, y) => Math.Clamp(x.Number - y.Number, -1, 1));
    }

    public void OnGetSortByDeparture()
    {
        OnGet();
        
        Route.Sort((x, y) => Math.Clamp((int)(x.Cities.First().DepartureTime - y.Cities.First().DepartureTime).Value.TotalMilliseconds, -1, 1));
    }

    public void OnGetSortByArrival()
    {
        OnGet();
        
        Route.Sort((x, y) =>
        {
            TimeSpan? totalDuration = x.Cities.Last().ArrivalTime - y.Cities.Last().ArrivalTime;
            
            return Math.Clamp((int)totalDuration.Value.TotalMilliseconds, -1, 1);
        });
    }

    public void OnGetSortByDuration()
    {
        OnGet();

        Route.Sort((x, y) =>
        {
            TimeSpan? xDuration = x.Cities.Last().ArrivalTime - x.Cities.First().DepartureTime;
            TimeSpan? yDuration = y.Cities.Last().ArrivalTime - y.Cities.First().DepartureTime;
            TimeSpan? totalDuration = xDuration - yDuration;

            return Math.Clamp((int)totalDuration.Value.TotalMilliseconds, -1, 1);
        });
    }

    private void RetrieveAllRoutes()
    {
        Route = _context.Route
            .Include(r => r.Cities)
            .Include(r => r.Tickets)
            .ToList();
    }

    private void FilterRoutesByFrom()
    {
       
        Route.ForEach(r => r.Cities = r.Cities
            .SkipWhile(c => c.Name != from)
            .ToList());

        Route.RemoveAll(r => r.Cities.Count < 2);
    }
    
    private void FilterRoutesByTo()
    {
       
        Route.ForEach(r => r.Cities = r.Cities
            .Reverse().SkipWhile(c => c.Name != to)
            .Reverse().ToList());

        Route.RemoveAll(r => r.Cities.Count < 2);
    }

    private void FilterRoutesByDate()
    {
        Route.RemoveAll(r => r.Cities.First().DepartureTime.Value.DayOfYear != date.DayOfYear);
    }
}