using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class WyjazdyDoKlientow
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PracownikId { get; set; }
        [Required]
        public int KlienciId { get; set; }
        [Required]
        public DateTime DataWyjazdu { get; set; }
        public TimeSpan? GodzinaOd { get; set; }
        public TimeSpan? GodzinaDo { get; set; }
        public Pracownik pracownik { get; set; }
        public Klient klienci { get; set; }
    }
}