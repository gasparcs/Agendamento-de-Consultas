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

   public AdicionarClienteDTO Cliente{get;set;}=null!;
   public AdicionarCliente_PacienteDTO Cliente_Paciente{get;set;}=null!;
   public AdicionarGeneroDTO  Genero{get;set;}=null!;
   

}
public class AdicionarClienteDTO
{
    public int Id_cliente{get;set;}
}
public class AdicionarCliente_PacienteDTO
{
    public int Id_cliente_paciente{get;set;}
}
public class AdicionarGeneroDTO
{
    public int Id_genero{get;set;}
}

