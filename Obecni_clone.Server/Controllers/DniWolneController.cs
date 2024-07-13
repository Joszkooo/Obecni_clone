using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obecni_clone.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DniWolneController: ControllerBase
    {
        private readonly IDniWolneService _dniWolneService;

        public DniWolneController(IDniWolneService dniWolneService)
        {
            _dniWolneService = dniWolneService;
        }

        [HttpGet]
        [Route("GetDniWolne")]
        public async Task<JsonResult> GetDniWolne()
        {
            return new JsonResult(await _dniWolneService.GetDniWolne());
        }

        [HttpDelete]
        [Route("DeleteWolne")]
        public async Task<JsonResult> DeleteWolne(string kiedy)
        {
            return new JsonResult(await _dniWolneService.DeleteWolne(kiedy));
        }
    }
}