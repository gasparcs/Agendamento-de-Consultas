using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Paciente;

public class ListarPacientesRepository(KigramedDbContext context) : IListagemRepository<PacienteModel>
{
    public async Task<IEnumerable<PacienteModel>> Listagem(int pagina = 1, int quantidade = 20)
    {
        var pacientes = await context.Tabelatb12_paciente.Include(p => p.Cliente).Include(c => c.ClientePaciente).Include(a => a.Consultas).ThenInclude(c => c.EstadoConsulta)
        .Include(g => g.Genero)
        .Skip((pagina - 1) * quantidade)
        .Take(quantidade)
        .ToListAsync();
        return pacientes;
    }
}
