using System;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.FuncionarioUseCase.Comand;

public class RemoverFuncionario(IRemoverRepository<FuncionarioModel> repository)
{
    public async Task<string> ExecuteAsync(string nif) 
    {
        var model = new FuncionarioModel
        { 
             Nif = nif
        };

        return await repository.RemoverAsync(model);
    }
}
