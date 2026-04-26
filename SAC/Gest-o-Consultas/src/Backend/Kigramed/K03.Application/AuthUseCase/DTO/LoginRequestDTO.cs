using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.AuthUseCase.DTO;

public class LoginRequestDTO
{
 
    [Required]
    [MaxLength(20)]
    public string Nif { get; set; } = string.Empty;

    [Required]
    [MinLength(4)]
    [MaxLength(100)]
    public string Senha { get; set; } = string.Empty;
}

