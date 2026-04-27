using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D13.EstadoConsulta;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.EstadoConsulta;

public class ListarEstadoConsulta(KigramedDbContext context) : IListagemRepository<EstadoConsultaModel>
{


    public async Task<IEnumerable<EstadoConsultaModel>> Listagem(int pagina = 1, int quantidade = 20)
    {
       return await context.Tabelatb13_estado_consulta
            .OrderBy(e => e.Descricao) 
            .ToListAsync();
    }

}
