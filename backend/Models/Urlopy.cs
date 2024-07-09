using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace best_back.Server.Models
{
    public class Urlopy
    {
        // NULLE OGARNIJ
        public int Id { get; set; }
        public int IdPracownika { get; set; }
        public DateTime Od_Kiedy { get; set; }
        public DateTime Do_Kiedy { get; set; }
        public int? DlugoscUrlopu { get; set; }
    }
}