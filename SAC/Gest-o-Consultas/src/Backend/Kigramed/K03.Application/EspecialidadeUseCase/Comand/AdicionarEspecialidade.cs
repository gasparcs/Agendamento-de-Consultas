using System;
using Kigramed.K03.Application.EspecialidadeUseCase.DTO;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.EspecialidadeUseCase.Comand;

public class AdicionarEspecialidade(IAdicionarRepository<EspecialidadeModel> repository)
{
    public async Task<string> ExecuteAsync(AdicionarEspecialidadeDTO dto)
    {
        var model = new EspecialidadeModel
        {
          Nome = dto.EspecialidadeNome,

          Descricao = dto.EspecialidadeDescricao,

          Estado = dto.EspecialidadeEstado
        };

        return await repository.AddAsync(model);
    }
}
