using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Consulta;

public class ListarConsultaRepository(KigramedDbContext context) : IListagemRepository<ConsultaModel>
{
    public async Task<IEnumerable<ConsultaModel>> Listagem()
    {
        var consultas = await context.Tabelatb15_consulta
        .Include(me=>me.MedicoConsulta)
        .Include(s=>s.Servico)
        .Include(p=>p.Paciente)
        .ToListAsync();
        return consultas;

    }
}
