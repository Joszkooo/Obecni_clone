using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obecni_clone.Server.Dtos.HD;

namespace Obecni_clone.Server.Services.HDService
{
    public class ListaHDService: IListaHDService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ListaHDService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<HdPracownikResponse>> GetListaHD(string dataHD)
        {
            DateOnly.TryParse(dataHD, out DateOnly parsedDate);
            var serviceResponse = new ServiceResponse<HdPracownikResponse>();
            try
            {
                var listaHD = _context.HDs.FirstOrDefault(x => x.Dzien == parsedDate);
                if ( listaHD is null )
                {
                    serviceResponse.Message = $"Brak HD na dzien: {dataHD}";
                    return serviceResponse;
                }

                var pracownik = _context.Pracownicy.FirstOrDefault(p => p.Id == listaHD.PracownikId);
                if ( pracownik is null )
                {
                    serviceResponse.Message = $"Brak pracownika z ID: {listaHD.PracownikId}";
                    return serviceResponse;
                }
                
                var response = new HdPracownikResponse
                    {
                        Id = pracownik.Id,
                        Imie = pracownik.Imie,
                        Nazwisko = pracownik.Nazwisko, 
                        DataHD = parsedDate
                    };
                    serviceResponse.Data = response;
                    return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error: {ex.Message}";
                return serviceResponse;
            }
        }
    }
}