using Kigramed.K04.Domain.D07.MedicoEspecialidade;

namespace Kigramed.K03.Application.MedicoEspecialidadeUseCase.Queries;

public class ListarMedicosEspecialidades(IListarMedicoEspecialidadeRepository repository)
{
    public async Task<IList<MedicoEspecilidadeModel>> ExecuteAsync()
    {
        return await repository.ExecuteAsync();
    }
}
