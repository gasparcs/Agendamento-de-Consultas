using System;

namespace Kigramed.K03.Application.ConsultaUseCase.DTO;

public class ListarConsulta
{
    public int IdConsulta {get;set;}

     public string IdMedicoEspecialidade {get;set;} = null!;

    public string Servicos {get;set;} = null!;

    public string IdPaciente {get;set;} = null!;

     public string Cliente { get; set; } = null!; 
    
    public string IdEstado {get;set;} = null!;

    public DateTime DataConsulta {get;set;}
}

