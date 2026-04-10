using System;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ClienteUseCase.Queries;

public class PegarClientePeloNif(IPegarpeloNifReporitory<ClienteModel> repository)
{
    public async Task<LeituraDTO?> ExecuteAsync(string nif)
    {
        var cliente = await repository.PegarpeloNifAsync(nif);

        if (cliente is null) return null;

        return new LeituraDTO
        {
            ClienteNome = cliente.Nome,

            ClienteNif = cliente.Nif_cliente,

            Contactos = cliente.Contactos.Select( c => new ContactoDTO
            {
                Contacto = c.Contacto
            }),

            Pacientes = cliente.Pacientes.Select( p => new PacienteDTO
            {
                Nome = p.Nome
            })
        };
      
    }
}
