using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Servico;

public class PegarTextoServicoRepository(KigramedDbContext context) : IPegarpeloTextoRepository<ServicoModel>
{
    public async Task<IEnumerable<ServicoModel>> PegarAsync(string texto)
    {
        return await context.Tabelatb08_servico
        .Where(s=>s.Nome.Contains(texto))
        .Include(e=>e.Especialidade)
        .ToListAsync();
    }
}
