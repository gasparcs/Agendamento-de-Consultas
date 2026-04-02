using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Especialidade;

public class ListarEspecialidadeRepository(KigramedDbContext context) : IListagemRepository<EspecialidadeModel>
{
    public async Task<IEnumerable<EspecialidadeModel>> Listagem()
    {
         var especialidade =await context.Tabelatb06_especialidade.Include(c =>c.MedicoEspecialidades).Include(s =>s.Servicos).ToListAsync();
        return especialidade;
    }
}
