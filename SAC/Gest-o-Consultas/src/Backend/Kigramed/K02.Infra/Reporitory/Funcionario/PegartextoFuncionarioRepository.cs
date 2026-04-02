using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Funcionario;

public class PegartextoFuncionarioRepository(KigramedDbContext context) : IPegarpeloTextoRepository<FuncionarioModel>
{
    public async Task<IEnumerable<FuncionarioModel>> PegarAsync(string texto)
    {
        return await context.Tabelatb02_funcionario.Where(t => t.Nome.Contains(texto)).Include(p=> p.Perfil).Include(c => c.Contactos).ToListAsync();
         
    }
}
