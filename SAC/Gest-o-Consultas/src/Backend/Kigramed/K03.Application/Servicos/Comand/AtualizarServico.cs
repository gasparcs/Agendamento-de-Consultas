using System;
using Kigramed.K03.Application.ServicoUseCase.DTO;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ServicoUseCase.Comand;

public class AtualizarServico(IActualizarRepository<ServicoModel> repository)
{
    public async Task<string> ExecuteAsync(AtualizarServicoDTO dto)
    {
        var model = new ServicoModel
        {
            Id = dto.ServicoId,

           Nome = dto.ServicoNome,

           Duracao_minuto = dto.ServicoDuracaoMinuto,

           Preco = dto.ServicoPreco,

           Estado = dto.ServicoEstado
        };

        return await repository.ActualizarAsync(model);
    }
}
