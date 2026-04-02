using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Consulta;

public class ListarConsultaRepository(KigramedDbContext context) : IListagemRepository<ConsultaModel>
{
    public Task<IEnumerable<ConsultaModel>> Listagem()
    {
    }
}
