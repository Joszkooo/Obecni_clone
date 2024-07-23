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

        public async Task<ServiceResponse<Urlop>> GetUrlop(int PracownikId)
        {
            var serviceResponse = new ServiceResponse<Urlop>();
            try
            {
                var dni = await _context.Urlopy.FirstOrDefaultAsync(x => x.PracownikId == PracownikId);
                if(dni is not null)
                {
                    serviceResponse.Data = dni;
                    serviceResponse.Message = "Znaleziono urlop";
                    return serviceResponse;
                }
                else {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Brak aktywnych urlopów";
                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error: {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<Urlop>> GetUrlopNotification(int PracownikId)
        {
            var serviceResponse = new ServiceResponse<Urlop>(); 
            try
            {
                var upComingUrlop = await _context.Urlopy
                .Where( u => u.PracownikId == PracownikId && u.Od_Kiedy >= DateTime.Now && u.Do_Kiedy <= DateTime.Now.AddDays(5))
                .FirstOrDefaultAsync();

                if (upComingUrlop is not null)
                {
                    serviceResponse.Data = upComingUrlop;
                    serviceResponse.Message = $"Zaczynasz urlop za {(upComingUrlop.Od_Kiedy - DateTime.Now).Days} dni";
                    return serviceResponse;
                }
                else
                {
                    var currentUrlop = await _context.Urlopy
                    .Where(u => u.PracownikId == PracownikId && DateTime.Now >= u.Od_Kiedy && DateTime.Now <= u.Do_Kiedy)
                    .FirstOrDefaultAsync();
                    if (currentUrlop != null)
                    {
                        serviceResponse.Data = currentUrlop;
                        serviceResponse.Message = $"Jesteś aktualnie na urlopie od {currentUrlop.Od_Kiedy:yyyy-MM-dd} do {currentUrlop.Do_Kiedy:yyyy-MM-dd}.";
                        return serviceResponse;
                    }
                    else
                    {
                        serviceResponse.Message = "Nie masz zaplanowanych urlopów na najbliższe 5 dni.";
                        return serviceResponse;
                    }
                }
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