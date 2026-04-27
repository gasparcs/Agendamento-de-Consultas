using System;
using Kigramed.K03.Application.PagamentoUseCase.DTO;
using Kigramed.K04.Domain.D14.Pagamento;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.PagamentoUseCase.Queries;

public class ListarPagamentos(IListagemRepository<PagamentoModel> repository)
{
    public async Task<IEnumerable<LeituraPagamento>> ExecuteAsync()
    {
        var pagamentos = await repository.Listagem();

        return pagamentos.Select(p => new LeituraPagamento
        {
            Id = p.Id,

            Cliente = p.Cliente.Nome,

            Secretaria = p.Funcionario.Nome,

            Comprovativo = p.Comprovativo,

            DataEnvio = p.Data_envio
        });
    }
}
