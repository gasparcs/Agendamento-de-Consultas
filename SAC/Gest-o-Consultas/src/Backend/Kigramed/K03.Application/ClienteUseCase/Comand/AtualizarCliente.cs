using System;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ClienteUseCase.Comand;

public class AtualizarCliente(IActualizarRepository<ClienteModel> repository)
{
    public async Task<string> ExecuteAsync(AtualizarClienteDTO dto)
    {
        var model = new ClienteModel
        {
            Nif_cliente = dto.ClienteNif,
            
            Nome = dto.ClienteNome
        };
        return await repository.ActualizarAsync(model);
    }
}
