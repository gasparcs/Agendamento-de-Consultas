using System;
using Kigramed.K03.Application.EstadoConsultaUseCase.DTO;
using Kigramed.K04.Domain.D13.EstadoConsulta;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.EstadoConsultaUseCase.Queries;

public class ListarEstados(IListagemRepository<EstadoConsultaModel> repository)
{
    public async Task<IEnumerable<LeituraEstadoConsulta>> ExecuteAsync()
    {
        var estados = await repository.Listagem();

        return estados.Select(p => new LeituraEstadoConsulta
            {
                Id = p.Id,

                Descricao = p.Descricao,
            
        });
    }
}
