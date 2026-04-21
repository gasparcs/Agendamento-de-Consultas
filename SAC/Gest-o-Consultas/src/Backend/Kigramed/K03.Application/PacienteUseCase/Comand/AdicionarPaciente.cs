using System;
using Kigramed.K03.Application.PacienteUseCase.DTO;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.PacienteUseCase.Comand;

public class AdicionarPaciente(IAdicionarRepository<PacienteModel> repository)
{
   public async Task<string>  ExecuteAsync(AdicionarPacienteDTO dto)
   {
      var model= new PacienteModel
      {
         Nome = dto.PacienteNome,
         Data_nascimento= dto.PacienteData_nascimento,
         Id_genero= dto.Genero.Id_genero,
         Id_cliente= dto.Cliente.Id_cliente,
         Id_cliente_paciente= dto.Cliente_Paciente.Id_cliente_paciente
      
      };
      return await repository.AddAsync(model);
   }
}
