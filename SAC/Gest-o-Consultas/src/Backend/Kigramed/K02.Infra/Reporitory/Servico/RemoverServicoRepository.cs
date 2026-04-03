using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Servico;

public class RemoverServicoRepository(KigramedDbContext context) : IRemoverRepository<ServicoModel>
{
    public async Task<string> RemoverAsync(ServicoModel model)
    {
        context.Tabelatb08_servico.Remove(model);
        return await context.SaveChangesAsync()>0?
        "Serviço removido com sucesso" :
        "Não foi possível remover serviço";
    }
}
