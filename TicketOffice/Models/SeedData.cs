using Microsoft.EntityFrameworkCore;
using TicketOffice.Data;

namespace TicketOffice.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context =
            new TicketOfficeContext(serviceProvider.GetRequiredService<DbContextOptions<TicketOfficeContext>>());

        if (context == null)
        {
            throw new ArgumentNullException("Null TicketOfficeContext");
        }

        if (context.User.Any() | context.Route.Any() | context.City.Any() | context.Ticket.Any())
        {
            return; // Data has been seeded
        }

        context.Database.EnsureCreated();

        context.User.AddRange(new User[]
        {
            new User
            {
                Email = "danylo.nazarko@nure.ua",
                Password = "*Hashed Password*",
                IsManager = false,
            },
            new User
            {
                Email = "ruslan.shanin@nure.ua",
                Password = "*Hashed Password*",
                IsManager = false
            }
        });
        
        context.Route.AddRange(new Route[]
        {
            new Route {
                Number = "0001",
                Capacity = 30,
                Cities = new City[]
                {
                    new City
                    {
                        Name = "Кремінна",
                        ArrivalTime = new DateTime(2022, 03, 28, 8, 15, 0),
                        DepartureTime = new DateTime(2022, 03, 28, 8, 35, 0),
                    },
                    new City
                    {
                        Name = "Рубіжне",
                        ArrivalTime = new DateTime(2022, 03, 28, 9, 5, 0),
                        DepartureTime = new DateTime(2022, 03, 28, 9, 25, 0),
                    },
                    new City
                    {
                        Name = "Сєвєродонецьк",
                        ArrivalTime = new DateTime(2022, 03, 28, 9, 55, 0)
                    }
                },
            },
            new Route
            {
                Number = "0002",
                Capacity = 25,
                Cities = new City[]
                {
                    new City
                    {
                        Name = "Сєвєродонецьк",
                        ArrivalTime = new DateTime(2022, 03, 28, 15, 55, 0),
                        DepartureTime = new DateTime(2022, 03, 28, 16, 15, 0),
                    },
                    new City
                    {
                        Name = "Рубіжне",
                        ArrivalTime = new DateTime(2022, 03, 28, 16, 45, 0),
                        DepartureTime = new DateTime(2022, 03, 28, 17, 5, 0),
                    },
                    new City
                    {
                        Name = "Кремінна",
                        ArrivalTime = new DateTime(2022, 03, 28, 17, 40, 0)
                    }
                }
            }
        });

        context.SaveChanges();
    }
}