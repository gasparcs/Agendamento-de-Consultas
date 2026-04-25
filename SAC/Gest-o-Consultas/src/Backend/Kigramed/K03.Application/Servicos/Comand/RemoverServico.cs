using System;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ServicoUseCase.Comand;

public class RemoverServico(IRemoverRepository<ServicoModel> repository)
{
    public async Task<string> ExecuteAsync(int id)
    {
        var model = new ServicoModel
        {
            Id = id
        };

        return await repository.RemoverAsync(model);

    }
}
