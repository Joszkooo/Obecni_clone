using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace best_back.Server.Models
{
    public class ListaHD
    {
        public int Id { get; set; }
        public int IdPracownika { get; set; }
        public DateOnly Kiedy { get; set; } // CO KIEDY ???
    }
}