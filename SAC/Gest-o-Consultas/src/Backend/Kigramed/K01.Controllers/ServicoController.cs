using Kigramed.K03.Application.ServicoUseCase.Comand;
using Kigramed.K03.Application.ServicoUseCase.DTO;
using Kigramed.K03.Application.ServicoUseCase.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController (AdicionarServico adicionarServices,
    AtualizarServico atualizarServices,
    RemoverServico removerServices,
    ListarServicos listarServices,
    PegarServicoPeloId pegaridServices,
    PegarServicoPeloTexto pegartextoServices)
    : ControllerBase
    {
         [HttpPost]
        public async Task<IActionResult> AdicionarServico(AdicionarServicoDTO dto)
        {
            if(!ModelState.IsValid)

            return StatusCode(400, ModelState);
            var resposta = await adicionarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta): 
            StatusCode(500, resposta);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarServico(int id, AtualizarServicoDTO dto)
        {
            if (!ModelState.IsValid)
            return StatusCode(400, ModelState);

            dto.ServicoId = id;    
            var resposta = await atualizarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(500, resposta);
        }

        [HttpDelete("{id}")]   
        public async Task<IActionResult> RemoverServico(int id)
        {
            var resposta = await removerServices.ExecuteAsync(id);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(404, resposta);
        }

         [HttpGet] 
        public async Task<IActionResult> ListarServicos()
        {
            var resposta = await listarServices.ExecuteAsync();
            return Ok(resposta);
        }

         [HttpGet("id/{id}")]
        public async Task<IActionResult> PegarServicoPeloId(int id)
        {
            var resposta = await pegaridServices.ExecuteAsync(id);
            return resposta is null ? StatusCode(404, "Servico não encontrado"):
            Ok(resposta);
        }

         [HttpGet("texto/{texto}")]
        public async Task<IActionResult> PegarServicoPeloTexto(string texto)
        {
            var resposta = await pegartextoServices.ExecuteAsync(texto);
            return resposta is null ? StatusCode(404, "Nenhum servico encontrado com o texto fornecido"):
            Ok(resposta);
        }
    }
}
