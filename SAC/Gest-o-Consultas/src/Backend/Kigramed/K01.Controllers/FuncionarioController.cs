using Kigramed.K03.Application.FuncionarioUseCase.Comand;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController 
    (AdicionarFuncionario adicionarServices)
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
    }
}
