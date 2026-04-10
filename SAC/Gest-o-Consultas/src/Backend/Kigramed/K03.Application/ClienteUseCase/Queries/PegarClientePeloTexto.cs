using System;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ClienteUseCase.Queries;

public class PegarClientePeloTexto(IPegarpeloTextoRepository<ClienteModel> repository)
{
    public async Task<IEnumerable<LeituraDTO>?> ExecuteAsync(string texto)
    {
        var cliente = await repository.PegarAsync(texto); 

        if(cliente is null) return null;

       return cliente.Select( c => new LeituraDTO
        {
            ClienteNome = c.Nome,

            ClienteNif = c.Nif_cliente,

            Contactos = c.Contactos.Select( ct => new ContactoDTO
            {
               Contacto = ct.Contacto
            }),

            Pacientes = c.Pacientes.Select( p => new PacienteDTO
            {
               Nome = p.Nome
            })

        });
    }
}
