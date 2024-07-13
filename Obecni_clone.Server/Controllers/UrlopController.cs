using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlopController: ControllerBase
    {
        private readonly IUrlopService _urlopService;

        public UrlopController(IUrlopService urlopService)
        {
            _urlopService = urlopService;
        }

        [HttpGet]
        [Route("GetUrlop")]
        public async Task<JsonResult> GetUrlop(int PracownikId)
        {
            return new JsonResult(await _urlopService.GetUrlop(PracownikId));
        }
    }
}