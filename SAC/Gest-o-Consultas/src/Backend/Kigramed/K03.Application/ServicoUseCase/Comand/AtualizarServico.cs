using System;
using Kigramed.K03.Application.ClienteUseCase.DTO;
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
            Nome = dto.ServicoNome,
            Preco = dto.ServicoPreco
        };
        return await repository.ActualizarAsync(model);
    }
}
