#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Models;

namespace TicketOffice.Data
{
    public class TicketOfficeContext : DbContext
    {
        public TicketOfficeContext(DbContextOptions<TicketOfficeContext> options)
            : base(options)
        {
        }

        public DbSet<TicketOffice.Models.User> User { get; set; }

        public DbSet<TicketOffice.Models.Route> Route { get; set; }

        public DbSet<TicketOffice.Models.City> City { get; set; }

        public DbSet<TicketOffice.Models.Ticket> Ticket { get; set; }
    }
}
