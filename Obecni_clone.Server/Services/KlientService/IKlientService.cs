using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Services.KlientService
{
    public interface IKlientService
    {
        Task<List<GetKlientResponse>> ShowKlienci();
    }
}