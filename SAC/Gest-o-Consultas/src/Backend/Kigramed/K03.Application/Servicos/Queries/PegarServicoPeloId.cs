using System;
using Kigramed.K03.Application.ServicoUseCase.DTO;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ServicoUseCase.Queries;

public class PegarServicoPeloId(IPegarpeloId<ServicoModel> repository)
{
     public async Task<ListarServicosDTO ?> ExecuteAsync(int id)
    {
      var servico = await repository.PegarAsync(id); 

      if(servico is null) return null;

      return new ListarServicosDTO
      {
          ServicoNome = servico.Nome,

          ServicoDuracaoMinuto = servico.Duracao_minuto,

          ServicoEstado = servico.Estado,

          ServicoPreco = servico.Preco,

          IdEspecialidade = servico.Id_especialidade
      };


    }
}
