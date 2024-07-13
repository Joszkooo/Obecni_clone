using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Services.UrlopService
{
    public class UrlopService: IUrlopService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UrlopService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<T> GetUrlop<T>(int PracownikId)
        {
            try
            {
                var dni = await _context.Urlopy.FirstOrDefaultAsync(x => x.PracownikId == PracownikId);
                if(dni is not null)
                {
                    return dni;
                }
                else return "Brak aktywnych urlopow";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}