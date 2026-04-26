using System;

namespace Kigramed.K03.Application.ConsultaUseCase.DTO;

public class AtualizarConsultaDTO
{
     
    public int Id{ get; set; } 
        
    public string Consulta_medico_especialiade{ get; set; } = string.Empty;
    
    public string Consultaservico{ get; set; } = string.Empty;

    public string Consultapaciente{ get; set; } = string.Empty;
   
    public bool ConsultaEstado{ get; set; } 

    public DateTime ConsultaData { get; set; }
}