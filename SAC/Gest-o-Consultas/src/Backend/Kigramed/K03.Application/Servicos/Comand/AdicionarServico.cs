using System;
using Kigramed.K03.Application.ServicoUseCase.DTO;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ServicoUseCase.Comand;

public class AdicionarServico(IAdicionarRepository<ServicoModel> repository)
{
    public async Task<string> ExecuteAsync(AdicionarServicoDTO dto)
    {
        var model = new ServicoModel
        {
            Nome = dto.ServicoNome,

            Duracao_minuto = dto.ServicoDuracaoMinuto,

            Preco = dto.ServicoPreco,

            Estado = dto.ServicoEstado,

            Id_especialidade = dto.IdEspecialidade
        };

        return await repository.AddAsync(model);
    }
}
