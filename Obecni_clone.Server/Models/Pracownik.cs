using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class Pracownik
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        public string? Pseudonim { get; set; } 
        [Required]
        public string Email { get; set; }
        [Required]
        public DateOnly DataZatrudnieniaOd { get; set; } // mozliwy blad; wczesniej typ byl DateTime
        public DateOnly? DataZatrudnieniaDo { get; set; }
        public bool? Moderator { get; set; }
        public ListaHD listaHD { get; set; }
        public Rejestr rejestr { get; set; }
    }
}