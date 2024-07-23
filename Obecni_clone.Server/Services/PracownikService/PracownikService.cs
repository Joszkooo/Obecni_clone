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

        public async Task<string> Verify(string email)
        {
            try
            {
                var mail = await _context.Pracownicy.FirstOrDefaultAsync(e => e.Email == email);
                
                if (mail is not null)
                {
                    return "Zweryfikowano wlasciwie";
                }
                else
                {
                    return "Brak weryfikacji";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
            
            
        }
    }
}