using System;
using Kigramed.K03.Application.ServicoUseCase.DTO;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.VisualBasic;

namespace Kigramed.K03.Application.ServicoUseCase.Queries;

public class ListarServicos(IListagemRepository<ServicoModel> repository)
{
    public async Task<IEnumerable<ListarServicosDTO>> ExecuteAsync()
    {
      var servicos = await repository.Listagem();

      return servicos.Select( s => new ListarServicosDTO
      {
        ServicoNome = s.Nome,

        ServicoDuracaoMinuto = s.Duracao_minuto,

        ServicoEstado = s.Estado,

        ServicoPreco = s.Preco,

        IdEspecialidade = s.Id_especialidade
      });
    }
 }   