using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class ListaHD
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PracownikId { get; set; }
        [Required]
        public DateOnly Dzien { get; set; } // dzien w ktorym pracownik jest na HD, wczesniej: 'Kiedy'
        public Pracownik pracownik { get; set; }
    }
}