using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusRejestr
    {
        Wejscie = 1,
        Wyjscie = 2,
        Przerwa = 3
    }
}