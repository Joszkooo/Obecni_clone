using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class DniWolne
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateOnly Dzien { get; set; } // dzien wolnego, wczesniej: 'Kiedy'
        public string? NazwaSwieta { get; set; }
    }
}