using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusMiejsca
    {
        W_biurze = 1,
        L4 = 2,
        Zdalnie = 3,
        Urlop = 4,
        Wyjazd_do_klienta = 5
    }
}