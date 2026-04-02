using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Funcionario;

public class AddFuncionarioRepository(KigramedDbContext context) : IAdicionarRepository<FuncionarioModel>
{
    public async Task<string> AddAsync(FuncionarioModel model)
    {
        await context.Tabelatb02_funcionario.AddAsync(model);
        return await context.SaveChangesAsync() > 0 ?
        "Funcionário cadastrado com sucesso" :
        "Não foi possível cadastrar funcionário";
    }
}
