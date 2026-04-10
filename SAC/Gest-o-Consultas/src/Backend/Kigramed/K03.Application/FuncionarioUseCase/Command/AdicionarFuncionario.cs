using System;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.FuncionarioUseCase.Command;

public class AdicionarFuncionario(IAdicionarRepository<FuncionarioModel> repository)
{

    public async Task<string> ExecuteAsync(AdicionarFuncionarioDTO dto)
    {
        var model = new FuncionarioModel
        {
            Nif = dto.FuncionarioNif,

            Id_Perfil = dto.FuncionarioIdPerfil,

            Nome = dto.FuncionarioNome,

            Estado = true
        };

        return await repository.AddAsync(model);
    }
}
