using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class Rejestr
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PracownikId { get; set; }
        [Required]
        public DateTime Wejscie { get; set; }
        public DateTime? Wyjscie { get; set; }
        [Required]
        public StatusRejestr Status { get; set; } // wyjscie, wejscie, l4 
        [Required]
        public StatusMiejsca? Status2 { get; set; } // zdalnie, w biurze, przerwa
        public Pracownik pracownik { get; set; }
    }
}