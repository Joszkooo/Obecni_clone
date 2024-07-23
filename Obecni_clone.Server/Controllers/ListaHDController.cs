using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obecni_clone.Server.Services.HDService;

namespace Obecni_clone.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListaHDController
    {
        private readonly IListaHDService _listaHDService;

        public ListaHDController(IListaHDService listaHDService)
        {
            _listaHDService = listaHDService;
        }

        [HttpGet]
        [Route("GetListaHD")]
        public async Task<JsonResult> GetListaHD(string dataHD)
        {
            return new JsonResult(await _listaHDService.GetListaHD(dataHD));
        }
    }
}