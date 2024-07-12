using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    public class Urlop
    {
        public int Id { get; set; }
        public int PracownikId { get; set; }
        public DateTime Od_Kiedy { get; set; }
        public DateTime Do_Kiedy { get; set; }
        public int? DlugoscUrlopu { get; set; }
        public Pracownik pracownik { get; set; }
    }
}