using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Dtos.Klient
{
    public class GetKlientResponse
    {
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
    }
}