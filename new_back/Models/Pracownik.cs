using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_back.Models
{
    
    public class Pracownik
    {
        public int Id { get; set; } = 0;
        public string Imie { get; set; } = "Imie";
        public string Nazwisko { get; set; } = "Nazwisko";
        public string Pseudonim { get; set; } = "Pseudonim";
        public string Email { get; set; } = "Email";
        public DateTime DataZatrudnieniaOd { get; set; } = DateTime.Now;
        public DateTime DataZatrudnieniaDo { get; set; } = DateTime.Now;
        public int Moderator { get; set; } = 0;
    }
}