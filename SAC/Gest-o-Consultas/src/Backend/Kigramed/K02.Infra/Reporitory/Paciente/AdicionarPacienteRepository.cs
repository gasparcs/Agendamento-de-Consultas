using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Paciente;

public class AdicionarPacienteRepository(KigramedDbContext context) : IAdicionarRepository<PacienteModel>
{
    public async Task<string> AddAsync(PacienteModel model)
    {
        await context.Tabelatb12_paciente.AddAsync(model);
        return await context.SaveChangesAsync() > 0 ?
        "Paciente cadastrado com sucesso." :
        "Não foi possível cadastrar o paciente.";
    }
}
