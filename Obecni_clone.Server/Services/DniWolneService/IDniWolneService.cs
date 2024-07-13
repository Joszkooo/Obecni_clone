using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Services.DniWolneService
{
    public interface IDniWolneService
    {
        Task<List<DniWolne>> GetDniWolne();
        Task<string> DeleteWolne(string dzien);
    }
}