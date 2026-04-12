using System;
using Kigramed.K03.Application.PacienteUseCase.DTO;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.PacienteUseCase.Queries;

public class ListarPacientes(IListagemRepository<PacienteModel> repository)
{
     public async Task<IEnumerable<LeituraPacienteDTO>> ExecuteAsync()
    {
        var pacientes = await repository.Listagem();

        return pacientes.Select(c => new LeituraPacienteDTO
        {
            PacienteNome = c.Nome,

            PacienteData_nasciemento = c.Data_nascimento,

            Consultas = c.Consultas.Select( ct => new ConsultaDTO
                {
                    ConsultaData_consulta = ct.Data_consulta

                }),
            
          

        });
    }
}
