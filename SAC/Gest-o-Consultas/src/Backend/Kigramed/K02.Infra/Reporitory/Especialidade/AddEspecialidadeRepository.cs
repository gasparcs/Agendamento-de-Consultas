using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Especialidade;

public class AddEspecialidadeRepository(KigramedDbContext context) : IAdicionarRepository<EspecialidadeModel>
{
    public async Task<string> AddAsync(EspecialidadeModel model)
    {
       await context.Tabelatb06_especialidade.AddAsync(model);
        return await context.SaveChangesAsync() > 0 ?
        "Funcionário cadastrado com sucesso" :
        "Não foi possível cadastrar funcionário";
    }
}
