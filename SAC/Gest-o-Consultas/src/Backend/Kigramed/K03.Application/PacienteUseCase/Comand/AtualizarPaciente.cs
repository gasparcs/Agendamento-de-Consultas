using System;
using Kigramed.K03.Application.PacienteUseCase.DTO;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.PacienteUseCase.Comand;

public class AtualizarPaciente(IActualizarRepository<PacienteModel> repository)
{
      public async Task<string> ExecuteAsync(AtualizarPacienteDTO dto)
    {
        var model = new PacienteModel
        {
            Nome = dto.PacienteNome,
            Data_nascimento = dto.PacienteData_nasciemento
        };
        return await repository.ActualizarAsync(model);
    }
}
