using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Obecni_clone.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KlientController : ControllerBase
    {
        private readonly IKlientService _klientService;

        public KlientController(IKlientService klientService)
        {
            _klientService = klientService;
        }

        [HttpGet]
        [Route("ShowKlienci")]
        public async Task<JsonResult> ShowKlienci()
        {
            return new JsonResult(await _klientService.ShowKlienci());
        }
    }
}