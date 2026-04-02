using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Consulta;

public class PegarConsultaRepository(KigramedDbContext context) : IPegarRepository<ConsultaModel>
{
    public async Task<IEnumerable<ConsultaModel>> PegarAsync()
    {
        await context.Tabelatb15_consulta.PegarAsync();
    }
}
