using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Especialidade;

public class PegaridEspecialidadeRepository(KigramedDbContext context) : IPegarpeloId<EspecialidadeModel>
{
    public async  Task<EspecialidadeModel> PegarAsync(int id)
    {
        return await context.Tabelatb06_especialidade.Include(n =>n.Servicos).Include(p =>p.MedicoEspecialidades).FirstAsync(s =>s.Id ==id);
         
    }
}
