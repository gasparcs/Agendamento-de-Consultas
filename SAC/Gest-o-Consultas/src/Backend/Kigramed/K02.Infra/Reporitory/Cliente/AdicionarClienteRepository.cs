using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Cliente;

public class AdicionarClienteRepository(KigramedDbContext context) : IAdicionarRepository<ClienteModel>
{
    public async Task<string> AddAsync(ClienteModel model)
    {
        await context.Tabelatb09_cliente.AddAsync(model);
        return await context.SaveChangesAsync() > 0 ?
        "Cliente cadastrado com sucesso." :
        "Não foi possível cadastrar o cliente.";
    }
}
