using Kigramed.K03.Application.ConsultaUseCase.Comand;
using Kigramed.K03.Application.ConsultaUseCase.DTO;
using Kigramed.K03.Application.ConsultaUseCase.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Medico")]
    public class MedicoController(
        ListarConsultas listarConsultasServices,
        AtualizarConsulta atualizarConsultaServices
    ) : ControllerBase
    {
        /// <summary>
        /// Lista todas as consultas do médico
        /// </summary>
        /// <returns>Lista de consultas com informações detalhadas</returns>
        [HttpGet("consultas")]
        public async Task<IActionResult> ListarConsultas()
        {
            try
            {
                var resposta = await listarConsultasServices.ExecuteAsync();
                return Ok(new
                {
                    mensagem = "Consultas listadas com sucesso",
                    dados = resposta
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao listar consultas",
                    erro = ex.Message
                });
            }
        }

        /// <summary>
        /// Atualiza o status de uma consulta
        /// </summary>
        /// <param name="id">ID da consulta</param>
        /// <param name="dto">Dados atualizados da consulta</param>
        /// <returns>Mensagem de sucesso ou erro</returns>
        [HttpPut("consulta/{id}")]
        public async Task<IActionResult> AtualizarConsulta(int id, [FromBody] AtualizarConsultaDTO dto)
        {
            // Validar estado do modelo
            if (!ModelState.IsValid)
                return StatusCode(400, ModelState);

            // Validar se o ID da URL corresponde ao DTO
            if (id != dto.Id)
                return StatusCode(400, new
                {
                    mensagem = "ID da consulta não corresponde ao corpo da requisição"
                });

            try
            {
                var resposta = await atualizarConsultaServices.ExecuteAsync(dto);

                if (resposta.Contains("sucesso"))
                    return StatusCode(200, new
                    {
                        mensagem = "Consulta atualizada com sucesso",
                        detalhes = resposta
                    });
                else
                    return StatusCode(400, new
                    {
                        mensagem = "Erro ao atualizar consulta",
                        detalhes = resposta
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao processar atualização",
                    erro = ex.Message
                });
            }
        }

        /// <summary>
        /// Endpoint de teste para verificar se a API do médico está respondendo
        /// </summary>
        /// <returns>Mensagem de status</returns>
        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new { mensagem = "API do Médico está operacional" });
        }
    }
}
