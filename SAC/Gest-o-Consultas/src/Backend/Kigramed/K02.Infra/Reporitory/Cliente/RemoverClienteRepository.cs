using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Cliente;

public class RemoverClienteRepository(KigramedDbContext context) : IRemoverRepository<ClienteModel>
{
    public async Task<string> RemoverAsync(ClienteModel model)
    {
        context.Tabelatb09_cliente.Remove(model);
        return await context.SaveChangesAsync() >0 ?
        "Cliente removido com sucesso." :
        "Não foi possível remover o cliente.";
    }
}
