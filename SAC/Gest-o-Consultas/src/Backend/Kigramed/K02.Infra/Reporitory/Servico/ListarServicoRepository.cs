using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Servico;

public class ListarServicoRepository(KigramedDbContext context) : IListagemRepository<ServicoModel>
{
    public async Task<IEnumerable<ServicoModel>> Listagem()
    {
        var servicos = await context.Tabelatb08_servico
        .Include(e=>e.Especialidade)
        .ToListAsync();
        return servicos;
    }
}
