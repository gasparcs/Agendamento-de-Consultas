using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Cliente;

public class AtualizarClienteRepository(KigramedDbContext context) : IActualizarRepository<ClienteModel>
{
    public async Task<string> ActualizarAsync(ClienteModel model)
    {
        var cliente = await context.Tabelatb09_cliente.FirstOrDefaultAsync(f=> f.Nif_cliente == model.Nif_cliente||f.Nome==model.Nome); 
        if(cliente is null) return "Cliente não encontrado";
        cliente.Nome = model.Nome;
         return await context.SaveChangesAsync() > 0 ?
         "Atualização feita com sucesso." :
         "Não foi possível efectuar a atualização.";   
    }
}
