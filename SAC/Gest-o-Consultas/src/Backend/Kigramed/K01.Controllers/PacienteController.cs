using Kigramed.K03.Application.PacienteUseCase.Comand;
using Kigramed.K03.Application.PacienteUseCase.DTO;
using Kigramed.K03.Application.PacienteUseCase.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController(AdicionarPaciente adicionarServices,
    AtualizarPaciente atualizarServices,
    RemoverPaciente removerServices,
    ListarPacientes listarServices)
    : ControllerBase
    {
         [HttpPost]
        public async Task<IActionResult> AdicionarPaciente(AdicionarPacienteDTO dto)
        {
            if(!ModelState.IsValid)
            
            return StatusCode(400, ModelState);

            var resposta = await adicionarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta) :
            StatusCode(500, resposta);            
        }

         [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPaciente(int id, AtualizarPacienteDTO dto)
        {
            if (!ModelState.IsValid)
                return StatusCode(400, ModelState);

            dto.IdPaciente = id;

            var resposta = await atualizarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(200, resposta)
            : StatusCode(404, resposta);
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverPaciente(int id)
        {
            var resposta = await removerServices.ExecuteAsync(id);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(404, resposta);
        }

         [HttpGet]
        public async Task<IActionResult> ListarPacientes()
        {
             var resposta = await listarServices.ExecuteAsync();
             return Ok(resposta);
        }
    }
}
