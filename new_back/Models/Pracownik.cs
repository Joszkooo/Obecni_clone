using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_back.Models
{
    
    public class Pracownik
    {
        public int Id { get; set; } = 0;
        public string Imie { get; set; } = "NULL";
        public string Nazwisko { get; set; } = "NULL";
        public string Pseudonim { get; set; } = "NULL";
        public string Email { get; set; } = "NULL";
        public DateTime DataZatrudnieniaOd { get; set; } = DateTime.Now;
        public DateTime? DataZatrudnieniaDo { get; set; }
        public int Moderator { get; set; } = 0;
    }
}