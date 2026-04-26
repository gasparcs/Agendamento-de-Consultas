using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Cliente;

public class ListarClientesRepository(KigramedDbContext context) : IListagemRepository<ClienteModel>
{
    public async Task<IEnumerable<ClienteModel>> Listagem(int pagina = 1, int quantidade = 20)
    {
        var clientes = await context.Tabelatb09_cliente
        .Include(p => p.Pacientes)
        .Include(c => c.Contactos).ThenInclude(c => c.TipoContacto)
        .Include(a => a.Pagamentos)
        .Skip((pagina - 1) * quantidade)
        .Take(quantidade)
        .ToListAsync();
        return clientes;
    }
}
