using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.AuthUseCase.DTO;

public class LoginRequestDTO
{
 
    [Required(ErrorMessage ="Nif é obrigatório")]
    [MaxLength(20)]
    public string Nif { get; set; } = string.Empty;

    [Required(ErrorMessage ="Senha é obrigatória")]
    [MinLength(4)]
    [MaxLength(100)]
    public string Senha { get; set; } = string.Empty;
}

