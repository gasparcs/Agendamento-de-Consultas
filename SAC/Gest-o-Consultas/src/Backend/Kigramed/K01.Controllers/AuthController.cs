using Kigramed.K03.Application.AuthUseCase.Comand;
using Kigramed.K03.Application.AuthUseCase.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        LoginUsuario loginUsuario
    ) : ControllerBase
    {
        /// <summary>
        /// Realiza o login de um usuário e retorna um token JWT
        /// </summary>
        /// <param name="request">Dados de login (NIF e Senha)</param>
        /// <returns>Token JWT e dados do usuário autenticado</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            // Validar estado do modelo
            if (!ModelState.IsValid)
                return StatusCode(400, ModelState);

            // Executar o caso de uso de login
            var resposta = await loginUsuario.ExecuteAsync(request);

            // Validar se o login foi bem-sucedido
            if (resposta == null)
                return StatusCode(401, new { mensagem = "NIF ou senha inválidos" });

            // Retornar sucesso com os dados do usuário e token
            return StatusCode(200, new
            {
                mensagem = "Login realizado com sucesso",
                dados = resposta
            });
        }

        /// <summary>
        /// Endpoint de teste para verificar se a API está respondendo
        /// </summary>
        /// <returns>Mensagem de status</returns>
        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new { mensagem = "API de Autenticação está operacional" });
        }
    }
}
