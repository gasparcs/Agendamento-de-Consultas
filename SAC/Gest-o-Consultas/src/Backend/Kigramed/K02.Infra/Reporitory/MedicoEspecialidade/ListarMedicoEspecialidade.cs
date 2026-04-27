using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D07.MedicoEspecialidade;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.MedicoEspecialidade;

public class ListarMedicoEspecialidade(KigramedDbContext context) : IListagemRepository<MedicoEspecilidadeModel>
{
      public async Task<IEnumerable<MedicoEspecilidadeModel>> Listagem(int pagina = 1, int quantidade = 20)
    {
       return await context.Tabelatb07_medico_especialidade
            .OrderBy(e => e.Funcionario) // ordena alfabeticamente
            .ToListAsync();
    }

}
