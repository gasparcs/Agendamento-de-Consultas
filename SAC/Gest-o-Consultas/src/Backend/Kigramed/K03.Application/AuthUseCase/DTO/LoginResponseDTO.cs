using System;

namespace Kigramed.K03.Application.AuthUseCase.DTO;

public class LoginResponseDTO
{
   
    
    public string Nome { get; set; } = string.Empty;
    public string Nif { get; set; } = string.Empty;
    public string Perfil { get; set; } = string.Empty;
    public string telefone { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

    public string role { get; set; } = string.Empty;

}
