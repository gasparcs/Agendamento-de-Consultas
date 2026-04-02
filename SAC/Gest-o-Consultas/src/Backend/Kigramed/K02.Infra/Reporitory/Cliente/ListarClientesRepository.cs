using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Cliente;

public class ListarClientesRepository(KigramedDbContext context) : IListagemRepository<ClienteModel>
{
    public async Task<IEnumerable<ClienteModel>> Listagem()
    {
        var clientes =await context.Tabelatb09_cliente.Include(p =>p.Pacientes).Include(c =>c.Contactos).Include(a => a.Pagamentos).ToListAsync();
        return clientes;
    }
}
