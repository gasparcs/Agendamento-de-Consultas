using System;
using System.ComponentModel.DataAnnotations;
using Kigramed.K03.Application.EspecialidadeUseCase.DTO;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Kigramed.K03.Application.EspecialidadeUseCase.Comand;

public class AtualizarEspecialidade(IActualizarRepository<EspecialidadeModel> repository)
{
    public async Task<string> ExecuteAsync(AtualizarEspecialidadeDTO dto)
    {
        var model = new EspecialidadeModel
        {
            Id = dto.EspecialidadeId,

            Nome = dto.EspecialidadeNome,

            Descricao = dto.EspecialidadeDescricao,

            Estado = dto.EspecialidadeEstado
        };

        return await repository.ActualizarAsync(model);
    }
}
