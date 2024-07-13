using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obecni_clone.Server.Dtos.Klient;

namespace Obecni_clone.Server.Services.KlientService
{
    public class KlientService : IKlientService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public KlientService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<GetKlientResponse>> ShowKlienci()
        {
            return await _context.Klienci.Select(x => new GetKlientResponse{Imie = x.Imie, Nazwisko = x.Nazwisko}).ToListAsync();
        }
    }
}