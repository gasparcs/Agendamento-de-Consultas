using System;
using Kigramed.K03.Application.ServicoUseCase.DTO;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ServicoUseCase.Queries;

public class ListarServicos(IListagemRepository<ServicoModel> repository)
{
   public async Task<IEnumerable<LeituraServicoDTO>> ExecuteAsync()
    {
        var servicos = await repository.Listagem();

      return servicos.Select(s => new LeituraServicoDTO
        {
            ServicoNome = s.Nome,

            ServicoPreco = s.Preco,

            Especialidades = new EspecialidadeDTO
            {
                Nome = s.Especialidade.Nome
            }

        });
    }
}
