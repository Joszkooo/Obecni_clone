using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace best_back.Server.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class PracownikController : ControllerBase
    {
        private readonly IPracownikService _pracownikService;

        public PracownikController(IPracownikService pracownikService)
        {
            _pracownikService = pracownikService;
        }

        [HttpGet] // to samo co w service ORAZ await wewnatrz zwracanych nawiasow
        [Route("GetAllPracownik")]
        public async Task<JsonResult> GetAllPracownik()
        {
            return new JsonResult(await _pracownikService.GetAllPracownik());
        }

        [HttpGet]
        [Route("GetRejestrPracownika")]
        public Task<JsonResult> GetRejestrPracownika(int idPracownika, string dzien)
        {
            return null;
        }

        [HttpGet]
        [Route("Verify")]
        public async Task<JsonResult> Verify(string email)
        {
            return new JsonResult(await _pracownikService.Verify(email));
        }

    }
}