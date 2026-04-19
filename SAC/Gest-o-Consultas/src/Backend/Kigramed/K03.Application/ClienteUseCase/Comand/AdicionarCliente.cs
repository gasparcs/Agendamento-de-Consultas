using System;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Kigramed.K04.Domain.D04.Contacto;
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
            
            Nif_cliente = dto.ClienteNif,

             Contactos = dto.Contactos.Select( c => new ContactoModel
            {
                Id_tipo_contacto = c.TipoContacto,

                Contacto = c.Contacto,

                Nif_funcionario = null,

                Nif_cliente = dto.ClienteNif
                
            }).ToList()

        };
         return await repository.AddAsync(model);
    }          
}
