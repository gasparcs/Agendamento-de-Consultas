using Kigramed.K03.Application.ConsultaUseCase.Comand;
using Kigramed.K03.Application.ConsultaUseCase.DTO;
using Kigramed.K03.Application.ConsultaUseCase.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController(AdicionarConsulta adicionarServices,
    ListarConsultas listarServices)
    : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AdicionarConsulta(AdicionarConsultaDTO dto)
        {
            if(
                !ModelState.IsValid
            )
            return StatusCode(400, ModelState);
            var resposta = await adicionarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta): 
             StatusCode(500, resposta);
        }

        [HttpGet]
        public async Task<IActionResult> ListarConsultas()
        {
            var resposta = await listarServices.ExecuteAsync();
            return Ok(resposta);
        }
    }
}
