using System;

namespace Kigramed.K03.Application.AuthUseCase.DTO;

public class LoginResponseDTO
{
   
    public int UsuarioId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Perfil { get; set; } = string.Empty;
    public string TipoUsuario { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

}
