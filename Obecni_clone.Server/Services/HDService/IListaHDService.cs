using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Services.HDService
{
    public interface IListaHDService
    {
        Task<ServiceResponse<HdPracownikResponse>> GetListaHD(string dataHD);
    }
}