using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_back.Models;

namespace new_back.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ) : base ( options )
        {
            
        }
        public DbSet<Pracownik> Pracownicy => Set<Pracownik>();
    }
}