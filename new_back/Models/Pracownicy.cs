using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_back.Models
{
    public class Pracownicy
    {
        public int Id { get; set; }

        public string Imie { get; set; } = null!;

        public string Nazwisko { get; set; } = null!;

        public string Pseudonim { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime DataZatrudnieniaOd { get; set; }

        public DateTime? DataZatrudnieniaDo { get; set; }

        public bool Moderator { get; set; }
    }
}
