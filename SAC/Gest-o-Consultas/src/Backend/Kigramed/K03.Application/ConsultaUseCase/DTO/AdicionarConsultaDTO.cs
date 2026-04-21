using System;

namespace Kigramed.K03.Application.ConsultaUseCase.DTO;

public class AdicionarConsultaDTO
{ 
    public int IdMedicoEspecialidade {get;set;}

    public int IdServico {get;set;}

    public int IdPaciente {get;set;}
    
    public int IdEstado {get;set;}

    public DateTime DataConsulta {get;set;}
}
