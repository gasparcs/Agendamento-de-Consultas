using Kigramed.K04.Domain.D13.EstadoConsulta;

namespace Kigramed.K03.Application.EstadoConsultaUseCase.Queries;

public class ListarEstadosConsulta(IListarEstadoConsultaRepository repository)
{
    public async Task<IList<EstadoConsultaModel>> ExecuteAsync()
    {
        return await repository.ExecuteAsync();
    }
}
