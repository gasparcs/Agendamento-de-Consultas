using System;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.FuncionarioUseCase.Comand;

public class AdicionarFuncionario(IAdicionarRepository<FuncionarioModel> repository)
{
    public async Task<string> ExecuteAsync(AdicionarFuncionarioDTO dto)
    {
        var model = new FuncionarioModel
        {
            Nif = dto.FuncionaioNif,

            Id_Perfil = dto.FuncionarioPerfil,

            Nome = dto.FuncionarioNome,

            Estado = dto.FuncionarioEstado
        };

        return await repository.AddAsync(model);
    }
}
