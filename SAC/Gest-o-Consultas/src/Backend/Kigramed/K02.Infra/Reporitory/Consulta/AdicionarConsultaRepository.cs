using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Consulta;

public class AdicionarConsultaRepository(KigramedDbContext context) : IAdicionarRepository<ConsultaModel>
{
    public async Task<string> AddAsync(ConsultaModel model)
    {
        await context.Tabelatb15_consulta.AddAsync(model);
        return await context.SaveChangesAsync()>0?
        "Consulta adicionada com sucesso" :
        "Não foi possível adicionar consulta";
    }
}
