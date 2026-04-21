using System;
using Kigramed.K03.Application.PacienteUseCase.DTO;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.PacienteUseCase.Queries;

public class PegarPacientePeloID(IPegarpeloId<PacienteModel> repository)
{
   public async Task<LeituraPacienteDTO?> ExecuteAsync(int id)
    {
         var paciente = await repository.PegarAsync(id);

         if (paciente is null) return null;

         return new LeituraPacienteDTO
         {
             PacienteId = paciente.Id,

             PacienteNome = paciente.Nome,

             PacienteData_nascimento = paciente.Data_nascimento,

             Cliente_Paciente = paciente.ClientePaciente.Descricao,

             Cliente = paciente.Cliente.Nome,

             Genero = paciente.Genero.Nome,

             Consultas = paciente.Consultas.Select( c =>  new ConsultaDTO
             {
                IdConsuta = c.Id,
                Data_Consulta= c.Data_consulta      
             }),

         };
    }
}
