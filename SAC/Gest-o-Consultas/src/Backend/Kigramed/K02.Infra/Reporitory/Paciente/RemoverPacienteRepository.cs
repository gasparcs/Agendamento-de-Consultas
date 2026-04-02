using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Paciente;

public class RemoverPacienteRepository(KigramedDbContext context) : IRemoverRepository<PacienteModel>
{
    public async Task<string> RemoverAsync(PacienteModel model)
    {
        context.Tabelatb12_paciente.Remove(model);
        return await context.SaveChangesAsync() >0 ?
        "Paciente removido com sucesso." :
        "Não foi possível remover o Paciente.";
    }
}
