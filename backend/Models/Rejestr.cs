using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace best_back.Server.Models
{
    public class Rejestr
    {
        // NULLE OGARNIJ
        public int Id { get; set; }
        public int IdPracownika { get; set; }
        public DateTime Wejscie { get; set; }
        public DateTime Wyjscie { get; set; }
        public string Status { get; set; } // MOZE ZMIANA NA BOOL
        public string? Status2 { get; set; } // PO CHUJ TO XD
    }
}