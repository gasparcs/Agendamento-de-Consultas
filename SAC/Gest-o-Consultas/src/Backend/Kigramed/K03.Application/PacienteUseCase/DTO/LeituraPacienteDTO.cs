using System;

namespace Kigramed.K03.Application.PacienteUseCase.DTO;

public class LeituraPacienteDTO
{
   public int PacienteId{get;set;}
   public string PacienteNome{get;set;}=string.Empty;
   public DateTime PacienteData_nascimento{get;set;}
   public ClienteDTO Cliente{get;set;}=null!;
   public Cliente_PacienteDTO Cliente_Paciente{get;set;}=null!;
   public GeneroDTO  Genero{get;set;}=null!;
   public IEnumerable<ConsultaDTO> Consultas{get;set;}=null!;
   

}
public class ClienteDTO
{
    public int Cliente{get;set;}
}
public class Cliente_PacienteDTO
{
    public int Id_cliente_paciente{get;set;}
}
public class GeneroDTO
{
    public int Id_genero{get;set;}
}

public class ConsultaDTO
{
    public int Consulta{get;set;}
}
