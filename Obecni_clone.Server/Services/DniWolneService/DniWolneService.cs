using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Services.DniWolneService
{
    public class DniWolneService: IDniWolneService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public DniWolneService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<DniWolne>> GetDniWolne()
        {
            return await _context.DniWolne.ToListAsync();
        }

        public async Task<string> DeleteWolne(string dzien)
        {
            try{
                DateOnly.TryParse(dzien, out DateOnly parsedDate);
                var wolneToRemove = await _context.DniWolne.Where(x => x.Dzien == parsedDate).ToListAsync();
                if (wolneToRemove.Any())
                {
                    _context.DniWolne.RemoveRange(wolneToRemove);
                    await _context.SaveChangesAsync();
                }
                
                return $"Usunięto wolne z dnia {dzien}";
            }
            catch(Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}