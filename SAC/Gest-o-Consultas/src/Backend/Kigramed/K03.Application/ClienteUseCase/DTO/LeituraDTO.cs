using System;
using System.Runtime.CompilerServices;

namespace Kigramed.K03.Application.ClienteUseCase.DTO;

public class LeituraDTO
{
    public string ClienteNif {get;set;} = string.Empty;

    public string ClienteNome {get;set;} = string.Empty;

    
    public IEnumerable<ContactoDTO> Contactos {get;set;} = [];

    public IEnumerable<PacienteDTO> Pacientes {get;set;} = [];
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

public class PacienteDTO
{
    public string Nome {get;set;} = string.Empty;
}
