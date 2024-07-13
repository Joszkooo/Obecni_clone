using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Dtos.Klient
{
    public class GetKlientResponse
    {
        public string Imie { get; set; } = null!;
        public string Nazwisko { get; set; } = null!;
    }
}