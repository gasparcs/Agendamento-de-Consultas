using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Paciente;

public class PegarPacientepeloIDRepository(KigramedDbContext context) : IPegarpeloId<PacienteModel>
{
    public async Task<PacienteModel?> PegarAsync(int id)
    {
        return await context.Tabelatb12_paciente.Include(p =>p.Cliente).Include(c =>c.ClientePaciente).Include(a => a.Consultas).Include(g => g.Genero).FirstOrDefaultAsync(i =>i.Id ==id);
    }
    
}
