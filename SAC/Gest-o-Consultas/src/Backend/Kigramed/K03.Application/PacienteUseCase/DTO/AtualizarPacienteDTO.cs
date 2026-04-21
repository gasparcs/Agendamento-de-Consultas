using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.PacienteUseCase.DTO;

public class AtualizarPacienteDTO
{
    [Required(ErrorMessage ="Nome do paciente é obrigatório")]
   public string PacienteNome{get;set;}=string.Empty;
   
   [Required(ErrorMessage ="Data de nascimento do paciente é obrigatória")]
   public DateTime PacienteData_Nascimento{get;set;}
}
