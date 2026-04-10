using System;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ClienteUseCase.Queries;

public class ListarClientes(IListagemRepository<ClienteModel> repository)
{
    public async Task<IEnumerable<LeituraDTO>> ExecuteAsync()
    {
      var clientes = await repository.Listagem();

      return clientes.Select(c => new LeituraDTO
        {
            ClienteNome = c.Nome,

            ClienteNif = c.Nif_cliente,

            Contactos = c.Contactos.Select( ct => new ContactoDTO
                {
                    Contacto = ct.Contacto

                }),
            
            Pacientes = c.Pacientes.Select(p => new PacienteDTO
                {
                    Nome = p.Nome

                })

        });
    }
}
