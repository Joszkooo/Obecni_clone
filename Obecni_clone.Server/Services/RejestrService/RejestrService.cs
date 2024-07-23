using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Services.RejestrService
{
    public class RejestrService: IRejestrService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public RejestrService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<Rejestr>> ChangeStatusRejestr(int PracownicyId)
        {
            var serviceResponse = new ServiceResponse<Rejestr>();
            try
            {
                var rejestrPracownika = await _context.Rejestry
                .Where(x => x.PracownikId == PracownicyId)
                .OrderByDescending(r => r.PracownikId)
                .FirstOrDefaultAsync();
                
                if(rejestrPracownika is not null && rejestrPracownika.Wyjscie is null)
                {
                    switch (rejestrPracownika.Status)
                    {
                        case StatusRejestr.Wejscie:
                            rejestrPracownika.Status = StatusRejestr.Wyjscie;
                            rejestrPracownika.Wyjscie = DateTime.Now.AddHours(2);
                            break;

                        case StatusRejestr.Wyjscie:
                            rejestrPracownika.Status = StatusRejestr.Przerwa;
                            break;
                        
                        case StatusRejestr.Przerwa:
                            rejestrPracownika.Status = StatusRejestr.Wejscie;
                            break;
                    }
                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Status został zmieniony";
                    return serviceResponse;
                }
                else
                {
                    var newStatus = new Rejestr{
                        PracownikId = PracownicyId,
                        Wejscie = DateTime.Now,
                        Wyjscie = null,
                        Status = StatusRejestr.Wejscie, // <= nie wiem czy enum sie przyjmie wiec w razie czego zmien tutaj  
                        Status2 = StatusMiejsca.W_biurze
                    };
                    await _context.AddAsync(newStatus);
                    serviceResponse.Message = "Status zostal dodany";
                    await _context.SaveChangesAsync();
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

        // nie dziala
        public async Task<ServiceResponse<List<Rejestr>>> GetRejestrPracownika(int idPracownika, string dzien)
        {
            var serviceResponse = new ServiceResponse<List<Rejestr>>();
            try
            {
                if (!DateOnly.TryParse(dzien, out DateOnly parsedDate)) // probowalem przekonwertowac ale chyba tutaj lezy problem
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Niepoprawny format daty";
                    return serviceResponse;
                }
                DateTime parsedDateTime = parsedDate.ToDateTime(TimeOnly.MinValue);

                var dbRejestrPracownika = await _context.Rejestry
                .Where( p => p.Id == idPracownika && p.Wejscie.Date == parsedDateTime.Date) // prawdopodobnie cos nie tak z tymi datami bo DateOnly i DateTime sie gryza
                .ToListAsync();

                if (dbRejestrPracownika is null || !dbRejestrPracownika.Any())
                {
                    serviceResponse.Message = "Brak wyniku";
                }
                else
                {
                    serviceResponse.Data = dbRejestrPracownika;
                    serviceResponse.Message = "Znaleziono pracownika";
                }
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error: {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<dynamic>> ShowStatus(int PracownikId)
        {
            var serviceResponse = new ServiceResponse<dynamic>();
            try
            {
                var response = await _context.Rejestry.FirstOrDefaultAsync(x => x.PracownikId == PracownikId);
                if (response is not null)
                {
                    serviceResponse.Data = new
                    {
                        response.Id,
                        response.PracownikId,
                        Wejscie = response.Wejscie.ToString("HH:mm:ss"), // Format for display
                        Wyjscie = response.Wyjscie?.ToString("HH:mm:ss"), // Format for display
                        response.Status,
                        response.Status2,
                        response.pracownik
                    };
                    return serviceResponse;
                }
                else{
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Brak aktywnych urlopow";
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

        public async Task<ServiceResponse<Rejestr>> ConfirmStatus(int PracownicyId)
        {
            var serviceResponse = new ServiceResponse<Rejestr>();
            try
            {
                var rejestrPracownika = await _context.Rejestry
                .Where(p => p.PracownikId == PracownicyId)
                .OrderByDescending(x => x.PracownikId)
                .FirstOrDefaultAsync();

                if (rejestrPracownika is null)
                {
                    serviceResponse.Message = "Brak rejestru w bazie ";
                    return serviceResponse;
                }
                else
                {
                    switch (rejestrPracownika.Status2)
                    {
                        case StatusMiejsca.W_biurze:
                            if (rejestrPracownika.Status == StatusRejestr.Wyjscie)
                            {
                                var newRejestr = new Rejestr
                                {
                                    PracownikId = PracownicyId,
                                    Wejscie = DateTime.Now.AddHours(2),
                                    Status = StatusRejestr.Wyjscie,
                                    Status2 = StatusMiejsca.W_biurze
                                };
                                
                                _context.Rejestry.Add(newRejestr);
                                await _context.SaveChangesAsync();
                                
                                serviceResponse.Message = "Status został zmieniony.";
                            }
                            else if (rejestrPracownika.Status == StatusRejestr.Przerwa)
                            {
                                rejestrPracownika.Wyjscie = null;
                                rejestrPracownika.Status = StatusRejestr.Wejscie;
                                await _context.SaveChangesAsync();
                                
                                serviceResponse.Message ="Praca kontunuowana po przerwie.";
                            }
                            else{
                                serviceResponse.Message = "Error! case StatusMiejsca.W_biurze";
                            }
                            break;
                            
                            
                        case StatusMiejsca.L4:
                        if (rejestrPracownika.Status == StatusRejestr.Wejscie || rejestrPracownika.Status == StatusRejestr.Przerwa)
                        {

                        }
                            break;
                        
                        case StatusMiejsca.Urlop:
                            serviceResponse.Message = "Brak możliwości zmiany statusu podczas urlopu";
                            break;
                        
                        case StatusMiejsca.Wyjazd_do_klienta:
                            serviceResponse.Message = "Brak możliwości zmiany statusu podczas wyjazdu do klienta";
                            break;

                        case StatusRejestr.Przerwa:
                            if(rejestrPracownika.Status == StatusRejestr.Wejscie)
                            {

                            }
                            else if(rejestrPracownika.Status == StatusRejestr.Wyjscie)
                            {

                            }
                            else{
                                serviceResponse.Message = "Error! case StatusRejestr.Przerwa";
                            }
                            break;

                        default:
                            serviceResponse.Message = $"Nie można zmienić aktualnego statusu {rejestrPracownika.Status2}";
                            break;
                    }
                    await _context.SaveChangesAsync();
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

        public async Task<ServiceResponse<Rejestr>> ChangeStatusMiejsca(int PracownicyId)
        {
            var serviceResponse = new ServiceResponse<Rejestr>();
            try
            {
                var rejestrPracownika = await _context.Rejestry
                .Where(x => x.PracownikId == PracownicyId)
                .OrderByDescending(r => r.PracownikId)
                .FirstOrDefaultAsync();
                
                if(rejestrPracownika is null)
                {
                    var newStatus = new Rejestr{
                        PracownikId = PracownicyId,
                        Wejscie = DateTime.Now,
                        Wyjscie = null,
                        Status = StatusRejestr.Wejscie, // <= nie wiem czy enum sie przyjmie wiec w razie czego zmien tutaj  
                        Status2 = StatusMiejsca.W_biurze
                    };
                    await _context.AddAsync(newStatus);
                    serviceResponse.Message = "Pracownik nie mial zadnego statusu, wiec ustawiono na prace w biurze";
                    await _context.SaveChangesAsync();
                    return serviceResponse;
                    
                }
                else
                {
                    switch (rejestrPracownika.Status2)
                    {
                        case StatusMiejsca.W_biurze:
                            rejestrPracownika.Status2 = StatusMiejsca.Zdalnie;
                            serviceResponse.Message = "Status został zmieniony na zdalną pracę";
                            break;

                        case StatusMiejsca.Zdalnie:
                            rejestrPracownika.Status2 = StatusMiejsca.L4;
                            serviceResponse.Message = "Status został zmieniony na l4";
                            break;

                        case StatusMiejsca.L4:
                            rejestrPracownika.Status2 = StatusMiejsca.W_biurze;
                            serviceResponse.Message = "Status został zmieniony na w biurze";
                            break;

                        case StatusMiejsca.Urlop:
                            serviceResponse.Message = "Nie możesz zmienić statusu w trakcie urlopu.";
                            break;

                        case StatusMiejsca.Wyjazd_do_klienta:
                            serviceResponse.Message = "Nie możesz zmienić statusu w trakcie wyjazdu do klienta.";
                            break;

                        default:
                            serviceResponse.Message = $"Nie można zmienić aktualnego statusu {rejestrPracownika.Status2}";
                            break;
                    }
                    await _context.SaveChangesAsync();
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

    }
}