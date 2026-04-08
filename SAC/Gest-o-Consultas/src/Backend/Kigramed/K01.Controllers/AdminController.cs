using Kigramed.K03.Application.ClienteUseCase.Comand;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController (AdicionarCliente adicionarServices): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(AdicionarClienteDTO dto)
        {
            if(
                ModelState.IsValid
            )
            return StatusCode(400, ModelState);
            var resposta = await adicionarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta): 
             StatusCode(500, resposta);
        }
    }
}
