using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_back.Models
{
    public class Klienci
    {
        public int id { get; set; } = 0;
        public string? imie { get; set; }
        public string? nazwisko { get; set; }
        public string? email { get; set; }
        public string? telefon { get; set; }
        public string? adres { get; set; }
        public string? miasto { get; set; }
        public string? kod_pocztowy { get; set; }
    }
}