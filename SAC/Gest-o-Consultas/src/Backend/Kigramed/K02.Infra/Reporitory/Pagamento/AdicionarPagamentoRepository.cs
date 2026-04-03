using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Pagamento;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Pagamento;

public class AdicionarPagamentoRepository(KigramedDbContext context) : IAdicionarRepository<PagamentoModel>
{
    public async Task<string> AddAsync(PagamentoModel model)
    {
        await context.Tabelatb14_pagamento.AddAsync(model);
        return await context.SaveChangesAsync()>0?
        "Pagamento adicionado com sucesso" :
        "Não foi possível adicionar pagamento";
    }
}
