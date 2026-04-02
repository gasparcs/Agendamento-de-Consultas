using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Consulta;

public class RemoverConsultaRepository(KigramedDbContext context) : IRemoverRepository<ConsultaModel>
{
    public async Task<string> RemoverAsync(ConsultaModel model)
    {
       context.Tabelatb15_consulta.Remove(model);
       return await context.SaveChangesAsync()>0?
       "Consulta remomvida com sucesso" :
       "Não foi possível remover a consulta";
    }
}
