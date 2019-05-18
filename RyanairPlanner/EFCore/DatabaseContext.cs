using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RyanairPlanner.Models;

namespace RyanairPlanner.EFCore
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AirportModel> Airports { get; set; }

        public DbSet<RouteModel> Routes { get; set; }

        public DbSet<UpdatesHistoryModel> UpdatesHistory { get; set; }

        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            :base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
