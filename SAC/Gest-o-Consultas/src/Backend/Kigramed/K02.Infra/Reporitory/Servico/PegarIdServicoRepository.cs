using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kigramed.K02.Infra.Reporitory.Servico;

public class PegarIdServicoRepository(KigramedDbContext context) : IPegarpeloId<ServicoModel>
{
    public async Task<ServicoModel> PegarAsync(int id)
    {
        return await context.Tabelatb08_servico.Include(e=>e.Especialidade).FirstAsync(s=>s.Id==id);

    }
}
