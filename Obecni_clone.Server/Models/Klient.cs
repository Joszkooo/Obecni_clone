using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class Klient
    {
        public int Id { get; set; }
        public string Imie { get; set; } = null!;
        public string Nazwisko { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefon { get; set; } = null!;
        public string Adres { get; set; } = null!;
        public string Miasto { get; set; } = null!;
        public string Kod_pocztowy { get; set; } = null!;
    }
}