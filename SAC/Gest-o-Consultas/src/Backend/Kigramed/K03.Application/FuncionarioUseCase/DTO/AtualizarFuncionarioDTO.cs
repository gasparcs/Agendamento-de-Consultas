using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.FuncionarioUseCase.DTO;

public class AtualizarFuncionarioDTO
{
   
    public string FuncionaioNif { get; set; } = string.Empty;

    public int FuncionarioPerfil { get; set; }

    [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
    public string FuncionarioNome { get; set; } = string.Empty;

    public bool FuncionarioEstado { get; set; }

     public IEnumerable<AtualizarFuncionarioEspecialidadeDTO> Especialidades { get; set; } = [];

    public IEnumerable<AtualizarContactoDTO> Contactos { get; set; } = [];
}

public class AtualizarContactoDTO
{
    public int TipoContacto { get; set; }

    public string Contacto { get; set; } = string.Empty;
}

public class AtualizarFuncionarioEspecialidadeDTO
{
    public int IdEspecialidade { get; set; }
}
