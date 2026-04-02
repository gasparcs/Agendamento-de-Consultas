using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Funcionario;

public class RemoverFuncionarioRepository(KigramedDbContext context) : IRemoverRepository<FuncionarioModel>
{
    public async Task<string> RemoverAsync(FuncionarioModel model)
    {
        context.Tabelatb02_funcionario.Remove(model);
        return await context.SaveChangesAsync() >0 ?
        "Funcionario removido com sucesso" :
        "Não foi possível remover";

    }
}
