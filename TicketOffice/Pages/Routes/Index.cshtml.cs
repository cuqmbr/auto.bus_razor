using Microsoft.AspNetCore.Mvc;
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

    public IndexModel(TicketOfficeContext context)
    {
        _context = context;
    }

    public ActionResult OnGet()
    {
        GetRoutes();
        return Page();
    }

    public ActionResult OnPost()
    {
        if (!PassengerNameValidation(Ticket.PassengerLastName,
                out PassengerLastNameValidationError) |
            !PassengerNameValidation(Ticket.PassengerFirstName,
                out PassengerFirstNameValidationError) |
            !PassengerPlaceValidation(Ticket.PassengerPlace,
                out PassengerPlaceValidationError))
            return OnGet();

        GetRoutes();
        CopyCitiesToTicket();
        _context.Ticket.Add(Ticket);
        RevertChangesToRouteCities();
        _context.SaveChanges();
       
        return RedirectToPage("/Auth/Account");
    }

    public void OnGetSortByNumber()
    {
        OnGet();

        if (SortString == "increasingNumber")
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
                totalDuration = x.Cities.First().DepartureTime -
                                y.Cities.First().DepartureTime;
            }
            else
            {
                totalDuration = y.Cities.First().DepartureTime -
                                x.Cities.First().DepartureTime;
            }

            return Math.Clamp((int) totalDuration.Value.TotalMilliseconds, -1,
                1);
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
                totalDuration = x.Cities.Last().ArrivalTime -
                                y.Cities.Last().ArrivalTime;
            }
            else
            {
                totalDuration = y.Cities.Last().ArrivalTime -
                                x.Cities.Last().ArrivalTime;
            }

            return Math.Clamp((int) totalDuration.Value.TotalMilliseconds, -1,
                1);
        });
    }

    public void OnGetSortByDuration()
    {
        OnGet();

        Routes.Sort((x, y) =>
        {
            TimeSpan? xDuration = x.Cities.Last().ArrivalTime -
                                  x.Cities.First().DepartureTime;
            TimeSpan? yDuration = y.Cities.Last().ArrivalTime -
                                  y.Cities.First().DepartureTime;
            TimeSpan? totalDuration;

            if (SortString == "increasingDuration")
            {
                totalDuration = xDuration - yDuration;
            }
            else
            {
                totalDuration = yDuration - xDuration;
            }

            return Math.Clamp((int) totalDuration.Value.TotalMilliseconds, -1,
                1);
        });
    }

    public List<string> GetCitiesNames(List<RouteCity> Cities)
    {
        List<string> citiesNames = new List<string>();

        foreach (var city in Cities)
        {
            citiesNames.Add(city.Name);
        }

        return citiesNames;
    }
    
    public List<string> GetCitiesNames(List<TicketCity> Cities)
    {
        List<string> citiesNames = new List<string>();

        foreach (var city in Cities)
        {
            citiesNames.Add(city.Name);
        }

        return citiesNames;
    }

    public int GetRemainingCapacity(Route route)
    {
        return route.Capacity - route.Tickets.Count(t => 
            GetCitiesNames(t.Cities.ToList())
                .Intersect(GetCitiesNames(route.Cities.ToList()))
                .ToList().Any());
    }
        

    private void RetrieveAllRoutes()
    {
        Routes = _context.Route
            .Include(r => r.Cities)
            .Include(r => r.Tickets)
            .ToList();

        // Add cities to tickets
        for (int i = 0; i < Routes.Count; i++)
        {
            for (int j = 0; j < Routes[i].Tickets.Count; j++)
            {
                Routes[i].Tickets.ToList()[j].Cities = _context.TicketCity
                    .Where(tc => tc.Ticket == Routes[i].Tickets.ToList()[j])
                    .ToList();
            }
        }
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
        Routes.RemoveAll(r =>
            r.Cities.First().DepartureTime.Value.DayOfYear != Date.DayOfYear);
    }

    private void GetRoutes()
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
    }

    private bool PassengerNameValidation(string? name,
        out string validationError)
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

        Ticket? ticket = _context.Ticket.FirstOrDefault(t =>
            t.RouteId == Ticket.RouteId &&
            t.PassengerPlace == Ticket.PassengerPlace);

        if (ticket is not null)
        {
            validationError = "Місце вже зайняте";
            return false;
        }

        validationError = String.Empty;
        return true;
    }

    private void CopyCitiesToTicket()
    {
        List<RouteCity> RouteCities = Routes.Find(r => r.Id == Ticket.RouteId).Cities.ToList();
        Ticket.Cities = new List<TicketCity>();
        foreach (var city in RouteCities)
        {
            Ticket.Cities.Add(new TicketCity
            {
                Name = city.Name,
                DepartureTime = city.DepartureTime,
                ArrivalTime = city.ArrivalTime
            });
        }
    }

    private void RevertChangesToRouteCities()
    {
        _context.ChangeTracker.Entries()
            .Where(e => e.Metadata.Name == "TicketOffice.Models.RouteCity")
            .ToList().ForEach(e => e.State = EntityState.Unchanged);
    }
}