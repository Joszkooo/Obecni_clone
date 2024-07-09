using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace best_back.Server.Services.PracownikService
{
    public interface IPracownikService
    {
        // Do wszystkich typów musisz dodać Task<> i tyle
        Task<List<Pracownik>> GetAllPracownik();
        Task<Pracownik> GetRejestrPracownika(int idPracownika, string dzien);
        Task<string> Verify(string email);
    }
}