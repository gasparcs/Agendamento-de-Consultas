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
            Id = dto.IdPaciente,

            Nome= dto.PacienteNome
        };
        return await repository.ActualizarAsync(model);
    }
}
