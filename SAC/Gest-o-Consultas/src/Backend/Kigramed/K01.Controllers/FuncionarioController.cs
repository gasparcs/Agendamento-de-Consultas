using Kigramed.K03.Application.FuncionarioUseCase.Comand;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Kigramed.K03.Application.FuncionarioUseCase.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController 
    (AdicionarFuncionario adicionarServices,
    ListarFuncionarios listarServices)
    : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AdicionarFuncionario(AdicionarFuncionarioDTO dto)
        {
            if(!ModelState.IsValid)
            
            return StatusCode(400, ModelState);

            var resposta = await adicionarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta) :
            StatusCode(500, resposta);            
        }

        [HttpGet]
        public async Task<IActionResult> ListarFuncionarios()
        {
             var resposta = await listarServices.ExecuteAsync();
             return Ok(resposta);
        }
    }
}
