using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace best_back.Server.Models
{
    public class WyjazdyDoKlientow
    {
        
        public int Id { get; set; }
        public int IdPracownika { get; set; }
        public int IdKlienta { get; set; }
        public DateTime DataWyjazdu { get; set; }
        public TimeSpan? GodzinaOd { get; set; }
        public TimeSpan? GodzinaDo { get; set; }
    }
}