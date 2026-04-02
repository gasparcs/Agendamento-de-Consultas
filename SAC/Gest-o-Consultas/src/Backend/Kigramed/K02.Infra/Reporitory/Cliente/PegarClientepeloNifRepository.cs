using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Cliente;

public class PegarClientepeloNifRepository(KigramedDbContext context) : IPegarpeloNifReporitory<ClienteModel>
{
    public async Task<ClienteModel?> PegarpeloNifAsync(string nif)
    {
     return await context.Tabelatb09_cliente.Include(p =>p.Pacientes).Include(c =>c.Contactos).Include(v => v.Pagamentos).FirstOrDefaultAsync(n =>n.Nif_cliente ==nif);
    }
}
