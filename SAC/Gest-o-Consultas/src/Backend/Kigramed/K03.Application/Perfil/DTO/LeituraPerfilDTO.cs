using System;

namespace Kigramed.K03.Application.Perfil.DTO;

public class LeituraPerfilDTO
{
    public string PerfilDescricao{get;set;}=string.Empty;
    public IEnumerable<FuncionarioDTO> Funcionarios{get;set;}=null!;
}

public class FuncionarioDTO
{
    public string FuncionarioNome{get;set;}=string.Empty;
    public string FuncionarioNif{get;set;}=string.Empty;
    
}
