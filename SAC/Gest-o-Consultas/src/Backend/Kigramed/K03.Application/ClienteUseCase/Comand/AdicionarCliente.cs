using System;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ClienteUseCase.Comand;

public class AdicionarCliente(IAdicionarRepository<ClienteModel> repository) 
{
    public async Task<string> ExecuteAsync(AdicionarClienteDTO dto)
    {
        var model = new ClienteModel
        {
            Nome = dto.ClienteNome,
            
            Nif_cliente = dto.ClienteNif

        };
         return await repository.AddAsync(model);
    }          
}
