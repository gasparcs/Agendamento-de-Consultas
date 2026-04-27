using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D13.EstadoConsulta;

namespace Kigramed.K02.Infra.Reporitory.EstadoConsulta;

public interface IListarEstadoConsultaRepository
{
    Task<IList<EstadoConsultaModel>> ExecuteAsync();
}

public class ListarEstadoConsultaRepository()
{
    public async Task<IList<EstadoConsultaModel>> ExecuteAsync()
    {
        return await Task.FromResult(context.Tabelatb13_estado_consulta.ToList());
    }
}