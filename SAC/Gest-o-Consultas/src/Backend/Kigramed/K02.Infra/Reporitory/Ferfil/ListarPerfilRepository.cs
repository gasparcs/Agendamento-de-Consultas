using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D01.Perfil;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Ferfil;

public class ListarPerfilRepository(KigramedDbContext context) : IListagemRepository<PerfilModel>
{
    public async Task<IEnumerable<PerfilModel>> Listagem()
    {
         var perfis = await context.Tabelatb01_perfil.Include(f =>f.Funcionarios)
         .Include(pf=>pf.PerfisPermissoes)
         .ToListAsync();
         return perfis;
         
    }
}
