using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Dtos.HD
{
    public class HdPracownikResponse
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        public DateOnly DataHD { get; set; }
    }
}