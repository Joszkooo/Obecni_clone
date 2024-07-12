using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class Rejestr
    {
        public int Id { get; set; }
        public int PracownikId { get; set; }
        public DateTime Wejscie { get; set; }
        public DateTime? Wyjscie { get; set; }
        public StatusRejestr Status { get; set; } // wyjscie, wejscie, l4 
        public StatusMiejsca Status2 { get; set; } // zdalnie, w biurze, przerwa
        public Pracownik pracownik { get; set; }
    }
}