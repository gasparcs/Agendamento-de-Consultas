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

    [Required(ErrorMessage = "Estado é obrigatório")]
    public bool FuncionarioEstado { get; set; }

    public IEnumerable<AdicionarFuncionarioEspecialidadeDTO> Especialidades { get; set; } = [];
   
    [Required(ErrorMessage = "Contactos são obrigatórios")]
    public IEnumerable<AdicionarContactoDTO> Contactos { get; set; } = [];

    public string FuncionarioAuth { get; set; } = string.Empty;
}

public class AdicionarContactoDTO
{
    public int TipoContacto { get; set; }

    public string Contacto { get; set; } = string.Empty;
}

public class AdicionarFuncionarioEspecialidadeDTO
{
    public int IdEspecialidade { get; set; }
}