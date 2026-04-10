using System;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ClienteUseCase.Comand;

public class RemoverCliente(IRemoverRepository<ClienteModel> repository)
{
    public async Task<string> ExecuteAsync(string nif) 
    {
        var model = new ClienteModel
        { 
             Nif_cliente = nif
        };

        return await repository.RemoverAsync(model);
    }
    
}
