using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class WyjazdyDoKlientow
    {
        public int Id { get; set; }
        public int PracownikId { get; set; }
        public int KlienciId { get; set; }
        public DateTime DataWyjazdu { get; set; }
        public TimeSpan? GodzinaOd { get; set; }
        public TimeSpan? GodzinaDo { get; set; }
        public Pracownik pracownik { get; set; }
        public Klienci klienci { get; set; }
    }
}