using System;
using Kigramed.K03.Application.ServicoUseCase.DTO;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ServicoUseCase.Queries;

public class PegarServicoPeloId(IPegarpeloId<ServicoModel> repository)
{
    public async Task<LeituraServicoDTO?> ExecuteAsync(int id)
    {
        var servicos = await repository.PegarAsync(id);
        if(servicos is null) return null;

      return new LeituraServicoDTO
        {
            ServicoNome = servicos.Nome,

            ServicoPreco = servicos.Preco,

            Especialidades = new EspecialidadeDTO
            {
                Nome = servicos.Especialidade.Nome
            }

        };
    }
}
