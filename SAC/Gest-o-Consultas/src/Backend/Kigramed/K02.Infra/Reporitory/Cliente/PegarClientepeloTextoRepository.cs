using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Cliente;

public class PegarClientepeloTextoRepository(KigramedDbContext context) : IPegarpeloTextoRepository<ClienteModel>
{
    public async Task<IEnumerable<ClienteModel>> PegarAsync(string texto)
    {
        return await context.Tabelatb09_cliente.Where(t => t.Nome.Contains(texto)).Include(p => p.Pacientes).Include(c => c.Contactos).Include(v => v.Pagamentos).ToListAsync();
    }
}
