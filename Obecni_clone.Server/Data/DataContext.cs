using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Obecni_clone.Server.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        // imo te nazwy sa troche zwalone, jesli ktos ma pomysl smialo zmieniac
        // po prostu zrobilem liczby mnogie ale nw xD
        public DbSet<DniWolne> DniWolne { get; set;}
        public DbSet<Klient> Klienci { get; set;}
        public DbSet<ListaHD> HDs { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }
        public DbSet<Rejestr> Rejestry { get; set; }
        public DbSet<Urlop> Urlopy { get; set; }
        public DbSet<WyjazdyDoKlientow> WyjazdyKlienci { get; set; }
    }
}