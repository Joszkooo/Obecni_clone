using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Pracownik
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pseudonim { get; set; }
        public string Email { get; set; }
        public DateTime DataZatrudnieniaOd { get; set; }
        public DateTime? DataZatrudnieniaDo { get; set; }
        public bool Moderator { get; set; }

        public ICollection<Rejestr> Rejestr { get; set; }
    }
}