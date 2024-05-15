using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_back.Services.PracownikService
{
    public interface IPracownikService
    {
        JsonResult GetPracownik();
        JsonResult ShowKlienci();
        
    }
}