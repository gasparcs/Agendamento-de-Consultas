using Kigramed.K03.Application.ClienteUseCase.Comand;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Kigramed.K03.Application.ClienteUseCase.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController (AdicionarCliente adicionarServices,
     AtualizarCliente atualizarServices,
      ListarClientes listarServices, 
      RemoverCliente removerServices,
      PegarClientePeloNif pegarnifServices,
      PegarClientePeloTexto pegartextoServices)
      : ControllerBase
    {
        //método adicionar
        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(AdicionarClienteDTO dto)
        {
            if(
                !ModelState.IsValid
            )
            return StatusCode(400, ModelState);
            var resposta = await adicionarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta): 
             StatusCode(500, resposta);
        }

        //método actualizar
        [HttpPut("{nif}")]
        public async Task<IActionResult> AtualizarCliente(string nif, AtualizarClienteDTO dto)
        {
            if (!ModelState.IsValid)
                return StatusCode(400, ModelState);

            dto.ClienteNif = nif;

            var resposta = await atualizarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(200, resposta)
            : StatusCode(404, resposta);
        }
        //método listar
        [HttpGet]
        public async Task<IActionResult> ListarCliente()
        {
            var resposta = await listarServices.ExecuteAsync();
            return Ok(resposta);
        }

        //método remover
        [HttpDelete("{nif}")]
        public async Task<IActionResult> RemoverCliente(string nif)
        {
            var resposta = await removerServices.ExecuteAsync(nif);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(404, resposta);
        }

        //método pegar pelo nif
        [HttpGet("nif/{nif}")]
        public async Task<IActionResult> PegarClientePeloNif(string nif)
        {
            var resposta = await pegarnifServices.ExecuteAsync(nif);
            return resposta is null ? StatusCode(404, "Cliente não encontrado"):
            Ok(resposta);
        }

        //método pegar pelo texto
        [HttpGet("texto/{texto}")]
        public async Task<IActionResult> PegarClientePeloTexto(string texto)
        {
            var resposta = await pegartextoServices.ExecuteAsync(texto);
            return resposta is null ? StatusCode(404, "Nenhum cliente encontrado") :
            Ok(resposta);
        }


    }
}
