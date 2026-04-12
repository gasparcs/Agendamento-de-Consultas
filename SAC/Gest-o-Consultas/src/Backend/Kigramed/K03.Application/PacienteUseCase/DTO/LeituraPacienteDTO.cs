using System;

namespace Kigramed.K03.Application.PacienteUseCase.DTO;

public class LeituraPacienteDTO
{

     public string PacienteNome{get;set;}=string.Empty;

     public DateTime PacienteData_nasciemento{get;set;}

         public IEnumerable<ConsultaDTO> Consultas{get;set;}=[];
        

}
public class ConsultaDTO
{
    public DateTime ConsultaData_consulta{get;set;}
}

