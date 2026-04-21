using System;
using System.ComponentModel.DataAnnotations;
using Kigramed.K04.Domain.D12.Paciente;
using Microsoft.AspNetCore.SignalR;

namespace Kigramed.K03.Application.PacienteUseCase.DTO;

public class AdicionarPacienteDTO
{
   [Required(ErrorMessage = "Nome é do paciente é obrigatório")]
   public string PacienteNome{get;set;}=string.Empty;

   [Required(ErrorMessage = "Data de nascimento do paciente é obrigatório")]
   public DateTime PacienteData_nascimento{get;set;}

   public string IdCliente{get;set;} = string.Empty;

   public int  IdCliente_Paciente {get;set;}


   [Required(ErrorMessage ="O gênero é obrigatório")]
   public int IdGenero {get;set;}

}



