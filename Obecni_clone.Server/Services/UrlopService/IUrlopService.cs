using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Services.UrlopService
{
    public interface IUrlopService
    {
        public Task<ServiceResponse<Urlop>> GetUrlop(int PracownikId);
        public Task<ServiceResponse<Urlop>> GetUrlopNotification(int UserId);
    }
}