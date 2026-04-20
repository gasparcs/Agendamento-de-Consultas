using System;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.EspecialidadeUseCase.Comand;

public class RemoverEspecialidade(IRemoverRepository<EspecialidadeModel> repository)
{
    public async Task<string> ExecuteAsync(int id)
    {
        var model = new EspecialidadeModel
        {
            Id = id
        };

        return await repository.RemoverAsync(model);
    }
}
