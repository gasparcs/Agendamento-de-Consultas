using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.FuncionarioUseCase.DTO;

public class AdicionarFuncionarioDTO
{
    [Required(ErrorMessage ="Nif é obrigatório")]
    public string FuncionarioNif {get;set;} = string.Empty;

    [Required(ErrorMessage ="Perfil é obrigatório")]
    public int FuncionarioIdPerfil {get;set;}

    [Required(ErrorMessage ="Nome é obrigatório")]
    public string FuncionarioNome {get;set;} = string.Empty;

    [Required(ErrorMessage ="Estado é obrigatório")]
    public string FuncionarioEstado {get;set;} = string.Empty;

}
