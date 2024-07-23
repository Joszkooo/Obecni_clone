using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Services.RejestrService
{
    public interface IRejestrService
    {
        Task<ServiceResponse<dynamic>> ShowStatus(int PracownikId);
        Task<ServiceResponse<List<Rejestr>>> GetRejestrPracownika(int idPracownika, string dzien);
        Task<ServiceResponse<Rejestr>> ChangeStatusRejestr(int PracownicyId);
        Task<ServiceResponse<Rejestr>> ChangeStatusMiejsca(int PracownicyId);
        Task<ServiceResponse<Rejestr>> ConfirmStatus(int PracownicyId);
    }
}