using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace best_back.Server.Models
{
    public class Pracownik
    {
        public int Id { get; set; }

        public string Imie { get; set; } = null!;

        public string Nazwisko { get; set; } = null!;

        public string? Pseudonim { get; set; }

        public string Email { get; set; } = null!;

        public DateOnly DataZatrudnieniaOd { get; set; } // zobacz czy sie nie zjebalo bo bylo wczesniej na DateTime

        public DateOnly? DataZatrudnieniaDo { get; set; }

        public bool Moderator { get; set; }
    }
}