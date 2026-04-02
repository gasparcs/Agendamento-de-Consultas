using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Consulta;

public class PegarIdConsultaRepository(KigramedDbContext context) : IPegarpeloId<ConsultaModel>
{
    public async Task<ConsultaModel> PegarAsync(int id)
    {
       return await context.Tabelatb15_consulta.
       Include(me =>me.MedicoConsulta)
       .Include(s=>s.Servico)
        .Include(p=>p.Paciente)
        .FirstAsync(c=> c.Id == id);
    }
}
