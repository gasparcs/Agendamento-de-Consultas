using Kigramed.K03.Application.EspecialidadeUseCase.Comand;
using Kigramed.K03.Application.EspecialidadeUseCase.DTO;
using Kigramed.K03.Application.EspecialidadeUseCase.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController(AdicionarEspecialidade adicionarServices,
    AtualizarEspecialidade atualizarServices,
    ListarEspecialidades listarServices,
    PegarEspecialidadePeloId pegaridServices,
    PegarEspecialidadePeloTexto pegartextoServices,
    RemoverEspecialidade removerServices
    ) 
    : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AdicionarEspecialidade(AdicionarEspecialidadeDTO dto)
        {
            if(!ModelState.IsValid)

            return StatusCode(400, ModelState);
            var resposta = await adicionarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso") ? StatusCode(201, resposta) :
            StatusCode(500, resposta); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarEspecialidade(int id, AtualizarEspecialidadeDTO dto)
        {
            if (!ModelState.IsValid)
            return StatusCode(400, ModelState);

            dto.EspecialidadeId = id;
            var resposta = await atualizarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(500, resposta);
        }

         [HttpGet] 
        public async Task<IActionResult> ListarEspecialidades()
        {
            var resposta = await listarServices.ExecuteAsync();
            return Ok(resposta);
        }

         [HttpGet("id/{id}")]
        public async Task<IActionResult> PegarEspecialidadePeloId(int id)
        {
            var resposta = await pegaridServices.ExecuteAsync(id);
            return resposta is null ? StatusCode(404, "Especialidade não encontrada"):
            Ok(resposta);
        }

         [HttpGet("texto/{texto}")]
        public async Task<IActionResult> PegarEspecialidadePeloTexto(string texto)
        {
            var resposta = await pegartextoServices.ExecuteAsync(texto);
            return resposta is null ? StatusCode(404, "Nenhuma especialidade encontrada com o texto fornecido"):
            Ok(resposta);
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverCliente(int id)
        {
            var resposta = await removerServices.ExecuteAsync(id);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(404, resposta);
        }

    }
}
