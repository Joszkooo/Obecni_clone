using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class Urlop
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PracownikId { get; set; }
        [Required]
        public DateTime Od_Kiedy { get; set; }
        [Required]
        public DateTime Do_Kiedy { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double DlugoscUrlopu { get; set; }
        public Pracownik pracownik { get; set; }
    }
}