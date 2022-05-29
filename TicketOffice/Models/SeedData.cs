using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;

namespace TicketOffice.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context =
            new TicketOfficeContext(serviceProvider
                .GetRequiredService<DbContextOptions<TicketOfficeContext>>());

        if (context == null)
        {
            throw new ArgumentNullException(nameof(serviceProvider));
        }

        if (context.User.Any() | context.Route.Any() |
            context.RouteCity.Any() | context.Ticket.Any()) // Data has been seeded
        {
            return;
        }

        context.Database.EnsureCreated();

        context.User.AddRange(new User[]
        {
            new User
            {
                Email = "danylo.nazarko@nure.ua",
                Password = "*Hashed Password*",
            }
        });
        
        context.Route.AddRange(new Route[]
        {
            new Route()
            {
                Number = 027,
                Capacity = 30,
                Cities = new List<RouteCity>()
                {
                    new RouteCity
                    {
                        Name = "Сватове",
                        
                        ArrivalTime = null,
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            6,
                            30,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Красноріченське",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            7,
                            10,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            7,
                            20,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Кремінна",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            7,
                            50,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            8,
                            0,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Рубіжне",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            8,
                            30,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            8,
                            40,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Сєвєродонецьк",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            9,
                            10,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            9,
                            20,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Лисичанськ",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            9,
                            50,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            12,
                            0,
                            0),
                    },
                    new RouteCity
                    {
                        Name = "Сєвєродонецьк",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            12,
                            30,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            12,
                            40,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Рубіжне",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            13,
                            10,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            13,
                            20,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Кремінна",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            13,
                            50,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            14,
                            0,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Красноріченське",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            14,
                            30,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            14,
                            40,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Сватове",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            15,
                            20,
                            0),
                
                        DepartureTime = null
                    }
                }
            },
            new Route()
            {
                Number = 013,
                Capacity = 25,
                Cities = new List<RouteCity>()
                {
                    new RouteCity
                    {
                        Name = "Кремінна",
                        
                        ArrivalTime = null,
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            7,
                            0,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Рубіжне",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            7,
                            30,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            7,
                            40,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Сєвєродонецьк",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            8,
                            10,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            8,
                            20,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Станиця Луганська",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            9,
                            20,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            11,
                            20,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Сєвєродонецьк",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            12,
                            20,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            12,
                            30,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Рубіжне",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            13,
                            0,
                            0),
                
                        DepartureTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            13,
                            10,
                            0)
                    },
                    new RouteCity
                    {
                        Name = "Кремінна",
                        
                        ArrivalTime = new DateTime(
                            DateTime.Today.Year, 
                            DateTime.Today.Month,
                            DateTime.Today.Day, 
                            13,
                            40,
                            0),
                
                        DepartureTime = null
                    }
                }
            }
        });

        context.SaveChanges();
    }
}