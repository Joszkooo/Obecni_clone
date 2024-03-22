using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Rejestr
    {
        public int Id { get; set; }
        public int PracownikId { get; set; }
        public Pracownik Pracownik { get; set; }
        public DateTime Wejscie { get; set; }
        public DateTime Wyjscie { get; set; }
        public string Status { get; set; }
    }
}