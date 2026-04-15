using Kigramed.K03.Application.PerfilUseCase.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController (ListarPerfis listarServices)
    : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ListarPerfis()
        {
            var resposta = await listarServices.ExecuteAsync();
            return Ok(resposta);
        }
    }
}
