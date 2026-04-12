using System;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Kigramed.K03.Application.PacienteUseCase.DTO;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.PacienteUseCase.Queries;

public class PegarPacientePeloID(IPegarpeloId<PacienteModel> repository)
{
    public async Task<LeituraPacienteDTO?> ExecuteAsync(int id)
    {
        var paciente = await repository.PegarAsync(id);

        if(paciente is null) return null;

        return new LeituraPacienteDTO
        {
            PacienteNome = paciente.Nome,

            PacienteData_nasciemento = paciente.Data_nascimento,

            Consultas = paciente.Consultas.Select( c => new ConsultaDTO
            {
                ConsultaData_consulta= c.Data_consulta
            })

            
        };
      
    }
}
