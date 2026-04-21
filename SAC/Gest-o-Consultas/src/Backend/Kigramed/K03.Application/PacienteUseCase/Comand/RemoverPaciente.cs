using System;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.PacienteUseCase.Comand;

public class RemoverPaciente(IRemoverRepository<PacienteModel> repository)
{
  public async Task<string> ExecuteAsync(int id)
    {
        var model= new PacienteModel
        {
            Id= id
            
        };
        return await repository.RemoverAsync(model);
    }
}
