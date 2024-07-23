using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class Klient
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(12)]
        public string Telefon { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Miasto { get; set; }
        [Required]
        [MaxLength(7)]
        public string Kod_pocztowy { get; set; }
    }
}