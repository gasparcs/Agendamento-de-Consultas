using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Especialidade;

public class PegartextoEspecialidadeRepository(KigramedDbContext context) : IPegarpeloTextoRepository<EspecialidadeModel>
{
    public async Task<IEnumerable<EspecialidadeModel>> PegarAsync(string texto)
    {
        return await context.Tabelatb06_especialidade.Where(t => t.Nome.Contains(texto)).Include(p=> p.Servicos).Include(c => c.MedicoEspecialidades).ToListAsync();
    }
}
