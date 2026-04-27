using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Paciente;

public class PegarPacientepeloTextoRepository(KigramedDbContext context) : IPegarpeloTextoRepository<PacienteModel>
{
    public async Task<IEnumerable<PacienteModel>> PegarAsync(string texto, int pagina = 1, int quantidade = 20)
    {
        try
        {
            return await context.Tabelatb12_paciente
                .Where(t => t.Nome.Contains(texto))
                .OrderBy(t => t.Nome)
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .Include(p => p.Cliente)
                .Include(c => c.ClientePaciente)
                .Include(a => a.Consultas)
                .ThenInclude(c => c.EstadoConsulta)
                .Include(g => g.Genero)
                .ToListAsync();
        }
        catch (Exception)
        {
            return [];
        }
    }
}
