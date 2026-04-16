using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.FuncionarioUseCase.DTO;

public class AdicionarFuncionarioDTO
{
    [Required(ErrorMessage = "Nif é obrigatório")]
    public string FuncionaioNif { get; set; } = string.Empty;

    [Required(ErrorMessage = "Perfil é obrigatório")]
    public int FuncionarioPerfil { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    public string FuncionarioNome { get; set; } = string.Empty;

    public bool FuncionarioEstado { get; set; }
}
