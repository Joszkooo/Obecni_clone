using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Services.PracownikService
{
    public interface IPracownikService
    {
        // Do wszystkich typów musisz dodać Task<> i tyle
        Task<List<Pracownik>> GetAllPracownik();
        Task<string> Verify(string email);
    }
}