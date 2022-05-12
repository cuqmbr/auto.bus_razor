﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;
using TicketOffice.Models;
using Route = TicketOffice.Models.Route;

namespace TicketOffice.Pages.Routes;

public class IndexModel : PageModel
{
    [BindProperty] public List<Route> Routes { get; set; }
    [BindProperty] public Ticket Ticket { get; set; }

    public string PassengerLastNameValidationError;
    public string PassengerFirstNameValidationError;
    public string PassengerPlaceValidationError;
    
    [BindProperty(SupportsGet = true)] public string From { get; set; }
    [BindProperty(SupportsGet = true)] public string To { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime Date { get; set; } = new DateTime(2022, 03, 28, 0, 0, 0).Date;
    [BindProperty(SupportsGet = true)] public string SortString { get; set; }

    private readonly TicketOfficeContext _context;

    public IndexModel(TicketOfficeContext context, ILogger<IndexModel> logger)
    {
        _context = context;
    }

    public ActionResult OnGet()
    {
        if (!string.IsNullOrWhiteSpace(From) || !string.IsNullOrWhiteSpace(To))
        {
            RetrieveAllRoutes();
        }
        
        if (!string.IsNullOrWhiteSpace(From))
        {
            FilterRoutesByFrom();
        }

        if (!string.IsNullOrWhiteSpace(To))
        {
            FilterRoutesByTo();
        }

        if (!string.IsNullOrWhiteSpace(From) || !string.IsNullOrWhiteSpace(To))
        {
            FilterRoutesByDate();
        }

        return Page();
    }

    public ActionResult OnPost()
    {
        if (!PassengerNameValidation(Ticket.PassengerLastName, out PassengerLastNameValidationError) | !PassengerNameValidation(Ticket.PassengerFirstName, out PassengerFirstNameValidationError) | !PassengerPlaceValidation(Ticket.PassengerPlace, out PassengerPlaceValidationError))
            return OnGet();
        
        _context.Ticket.Add(Ticket);
        _context.SaveChanges();
        
        return RedirectToPage("/Auth/Account");
    }
    
    public void OnGetSortByNumber()
    {
        OnGet();

        if (SortString == "increasingNumber dependencies")
        {
            Routes.Sort((x, y) => Math.Clamp(x.Number - y.Number, -1, 1));
        }
        else
        {
            Routes.Sort((x, y) => Math.Clamp(y.Number - x.Number, -1, 1));
        }
    }

    public void OnGetSortByDeparture()
    {
        OnGet();

        Routes.Sort((x, y) =>
        {
            TimeSpan? totalDuration;
            
            if (SortString == "increasingDeparture")
            {
                totalDuration = x.Cities.First().DepartureTime - y.Cities.First().DepartureTime;
            }
            else
            {
                totalDuration = y.Cities.First().DepartureTime - x.Cities.First().DepartureTime;
            }

            return Math.Clamp((int)totalDuration.Value.TotalMilliseconds, -1, 1);
        });
    }

    public void OnGetSortByArrival()
    {
        OnGet();

        Routes.Sort((x, y) =>
        {
            TimeSpan? totalDuration;

            if (SortString == "increasingArrival")
            {
                totalDuration = x.Cities.Last().ArrivalTime - y.Cities.Last().ArrivalTime;
            }
            else
            {
                totalDuration = y.Cities.Last().ArrivalTime - x.Cities.Last().ArrivalTime;
            }
            
            return Math.Clamp((int)totalDuration.Value.TotalMilliseconds, -1, 1);
        });
    }

    public void OnGetSortByDuration()
    {
        OnGet();

        Routes.Sort((x, y) =>
        {
            TimeSpan? xDuration = x.Cities.Last().ArrivalTime - x.Cities.First().DepartureTime;
            TimeSpan? yDuration = y.Cities.Last().ArrivalTime - y.Cities.First().DepartureTime;
            TimeSpan? totalDuration;

            if (SortString == "increasingDuration")
            {
                totalDuration = xDuration - yDuration;
            }
            else
            {
                totalDuration = yDuration - xDuration;
            }

            return Math.Clamp((int)totalDuration.Value.TotalMilliseconds, -1, 1);
        });
    }

    private void RetrieveAllRoutes()
    {
        Routes = _context.Route
            .Include(r => r.Cities)
            .Include(r => r.Tickets)
            .ToList();
    }

    private void FilterRoutesByFrom()
    {
       
        Routes.ForEach(r => r.Cities = r.Cities
            .SkipWhile(c => c.Name.ToLower() != From.Trim().ToLower())
            .ToList());

        Routes.RemoveAll(r => r.Cities.Count < 2);
    }
    
    private void FilterRoutesByTo()
    {
       
        Routes.ForEach(r => r.Cities = r.Cities
            .Reverse().SkipWhile(c => c.Name.ToLower() != To.Trim().ToLower())
            .Reverse().ToList());

        Routes.RemoveAll(r => r.Cities.Count < 2);
    }

    private void FilterRoutesByDate()
    {
        Routes.RemoveAll(r => r.Cities.First().DepartureTime.Value.DayOfYear != Date.DayOfYear);
    }

    private bool PassengerNameValidation(string? name, out string validationError)
    {
        if (String.IsNullOrEmpty(name))
        {
            validationError = "Поле має бути заповненим";
            return false;
        }

        validationError = String.Empty;
        return true;
    }

    private bool PassengerPlaceValidation(int place, out string validationError)
    {
        if (place == 0)
        {
            validationError = "Поле має бути заповненим";
            return false;
        }
        
        validationError = String.Empty;
        return true;
    }
}