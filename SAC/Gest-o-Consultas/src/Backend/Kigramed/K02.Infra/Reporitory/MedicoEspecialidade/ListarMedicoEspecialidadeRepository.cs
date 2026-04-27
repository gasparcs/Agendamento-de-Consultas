using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D07.MedicoEspecialidade;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.MedicoEspecialidade;

public interface IListarMedicoEspecialidadeRepository
{
    Task<IList<MedicoEspecilidadeModel>> ExecuteAsync();
}

public class ListarMedicoEspecialidadeRepository(KigramedDbContext context) : IListarMedicoEspecialidadeRepository
{
    public async Task<IList<MedicoEspecilidadeModel>> ExecuteAsync()
    {
        return await context.Tabelatb07_medico_especialidade
            .Include(m => m.Funcionario)
            .Include(m => m.Especialidade)
            .ToListAsync();
    }
}
