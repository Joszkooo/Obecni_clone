using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace best_back.Server.Services.PracownikService
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
            var dbPracownicy = await _context.Pracownicy.ToListAsync();
            return dbPracownicy;
        }

        public async Task<Pracownik> GetRejestrPracownika(int idPracownika, string dzien)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Verify(string email)
        {
            var mail = await _context.Pracownicy.FirstOrDefaultAsync(e => e.Email == email);
            
            if (mail != null)
            {
                return "Zweryfikowano wlasciwie";
            }
            else
            {
                return "Brak weryfikacji";
            }
            
        }
        // [HttpGet]
        // [Route("ShowClients")]
        // public JsonResult ShowKlienci()
        // {
        // }

    }
}