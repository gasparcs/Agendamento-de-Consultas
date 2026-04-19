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
    ListarFuncionarios listarServices,
    PegarFuncionarioPeloNif pegarnifServices,
    PegarFuncionarioPeloTexto pegartextoServices,
    AtualizarFuncionario atualizarServices,
    RemoverFuncionario removerServices)
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

        [HttpGet("nif/{nif}")] 
        public async Task<IActionResult> PegarFuncionarioPeloNif(string nif)
        {
            var resposta = await pegarnifServices.ExecuteAsync(nif);
            return resposta is null? StatusCode(404, "Funcionário não encontrado"):
            Ok(resposta);
        } 

        [HttpGet("texto/{texto}")]
        public async Task<IActionResult> PegarFuncionarioPeloTexto(string texto)
        {
            var resposta = await pegartextoServices.ExecuteAsync(texto);
            return resposta is null? StatusCode(404, "Nenhum funcionário encontrado"):
            Ok(resposta); 
        } 

        [HttpPut("{nif}")]
         public async Task<IActionResult> AtualizarFuncionario(string nif, AtualizarFuncionarioDTO dto)
        {
            if (!ModelState.IsValid)
            return StatusCode(400, ModelState);

            dto.FuncionaioNif = nif;

            var resposta = await atualizarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(200, resposta)
            : StatusCode(404, resposta);
        }

        [HttpDelete("{nif}")]
        public async Task<IActionResult> RemoverFuncionario(string nif)
        {
            var resposta = await removerServices.ExecuteAsync(nif);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(404, resposta);
        }


    }
}
