using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obecni_clone.Server.Services.RejestrService;

namespace Obecni_clone.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RejestrController: ControllerBase
    {
        private readonly IRejestrService _rejestrService;

        public RejestrController(IRejestrService rejestrService)
        {
            _rejestrService = rejestrService;
        }


        [HttpGet]
        [Route("GetRejestrPracownika")]
        public async Task<JsonResult> GetRejestrPracownika(int idPracownika, string dzien)
        {
            return new JsonResult(await _rejestrService.GetRejestrPracownika(idPracownika, dzien));
        }

        [HttpGet]
        [Route("ShowStatus")]
        public async Task<JsonResult> ShowStatus(int PracownikId)
        {
            return new JsonResult(await _rejestrService.ShowStatus(PracownikId));
        }

        [HttpPost]
        [Route("ChangeStatusRejestr")]
        public async Task<JsonResult> ChangeStatusRejestr(int PracownikId)
        {
            return new JsonResult(await _rejestrService.ChangeStatusRejestr(PracownikId));
        }

        [HttpPost]
        [Route("ChangeStatusMiejsca")]
        public async Task<JsonResult> ChangeStatusMiejsca(int PracownikId)
        {
            return new JsonResult(await _rejestrService.ChangeStatusMiejsca(PracownikId));
        }

        [HttpPost]
        [Route("ConfirmStatus")]
        public async Task<JsonResult> ConfirmStatus(int PracownikId)
        {
            return new JsonResult(await _rejestrService.ConfirmStatus(PracownikId));
        }
    }
}