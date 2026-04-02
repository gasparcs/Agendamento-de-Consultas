using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Especialidade;

public class RemoverEspecialidadeRepository(KigramedDbContext context) : IRemoverRepository<EspecialidadeModel>
{
    public async Task<string> RemoverAsync(EspecialidadeModel model)
    {
         context.Tabelatb06_especialidade.Remove(model);
        return await context.SaveChangesAsync() >0 ?
        "Especialidade removida com sucesso" :
        "Não foi possível remover";
    }
}
