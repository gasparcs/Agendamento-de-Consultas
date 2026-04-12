using System;
using Kigramed.K03.Application.ServicoUseCase.DTO;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ServicoUseCase.Queries;

public class PegarServicoPeloTexto(IPegarpeloTextoRepository<ServicoModel> repository)
{
     public async Task<IEnumerable<LeituraServicoDTO>?> ExecuteAsync(string texto)
    {
        var servicos = await repository.PegarAsync(texto);
        if(servicos is null) return null;

      return servicos.Select(s=>new LeituraServicoDTO
      {
            ServicoNome = s.Nome,

            ServicoPreco = s.Preco,
            
            Especialidades= new EspecialidadeDTO
            {
                Nome = s.Especialidade.Nome
            }

           
      });
       
    }
}
