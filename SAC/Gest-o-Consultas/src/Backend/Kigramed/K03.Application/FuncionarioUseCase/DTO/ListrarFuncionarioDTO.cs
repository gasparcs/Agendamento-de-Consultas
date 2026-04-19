using System;

namespace Kigramed.K03.Application.FuncionarioUseCase.DTO;

public class ListrarFuncionarioDTO
{
    public string FuncionarioNif {get;set;} = string.Empty;

    public string FUncionarioPerfil {get;set;} = null!;

    public string FuncionarioNome {get;set;} = string.Empty;

    public bool FuncionaroEstado {get;set;} 

    public IEnumerable<ContactoDTO> Contactos {get;set;} = [];
}

public class TipoContactoDTO
{
    public string Descricao {get;set;} = string.Empty;
}
public class ContactoDTO
{
    public TipoContactoDTO TipoContacto {get; set;} = null!;
    public string Contacto {get;set;} = string.Empty;
}

