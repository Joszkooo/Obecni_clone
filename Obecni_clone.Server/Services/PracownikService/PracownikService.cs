using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Services.PracownikService
{
    public class PracownikService : IPracownikService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PracownikService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        // tutaj tez wszedzie Task<> oraz async
        public async Task<List<Pracownik>> GetAllPracownik()
        {
            return await _context.Pracownicy.ToListAsync();
        }

        public async Task<Pracownik> GetRejestrPracownika(int idPracownika, string dzien)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Verify(string email)
        {
            var mail = await _context.Pracownicy.FirstOrDefaultAsync(e => e.Email == email);
            
            if (mail is not null) // zmienilem z != sprawdz czy git
            {
                return "Zweryfikowano wlasciwie";
            }
            else
            {
                return "Brak weryfikacji";
            }
            
        }
    }
}