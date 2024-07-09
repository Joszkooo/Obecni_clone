using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace best_back.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Pracownik> Pracownicy { get; set; } // => Set<Pracownik>();   ale chyba tak jak jest tez jest git
    }
}