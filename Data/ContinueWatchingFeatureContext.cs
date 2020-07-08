using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContinueWatchingFeature.Models;
using System.Security.Cryptography.X509Certificates;

namespace ContinueWatchingFeature.Data
{
    public class ContinueWatchingFeatureContext : DbContext
    {
        public ContinueWatchingFeatureContext(DbContextOptions<ContinueWatchingFeatureContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Still_Watching> Still_Watchings { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Epsoide> Epsoides { get; set; }
        
    }
}
