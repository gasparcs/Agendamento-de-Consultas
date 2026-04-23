using System;

namespace Kigramed.K03.Application.EspecialidadeUseCase.DTO;

public class ListarEspecialidadeDTO
{
    public int EspecialidadeId {get;set;} 

    public string EspecialidadeNome {get;set;} = string.Empty;

    public string EspecialidadeDescricao {get;set;} = string.Empty;

    public bool EspecialidadeEstado {get;set;}

    public IEnumerable<MedicoEspecialidadeDTO> MedicoEspecialidade {get;set;} = [];

    public IEnumerable<ServicoDTO> Servicos {get;set;} = [];
}

public class MedicoEspecialidadeDTO
{
    public string FuncionarioNif {get;set;} = null!;

    public string FuncionarioNome {get;set;} = null!;
}

public class ServicoDTO
{
    public string ServicoDescricao {get;set;} = string.Empty;
}
