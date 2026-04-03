using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Pagamento;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Pagamento;

public class RemoverPagamentoRepository(KigramedDbContext context) : IRemoverRepository<PagamentoModel>
{
    public async Task<string> RemoverAsync(PagamentoModel model)
    {
        context.Tabelatb14_pagamento.Remove(model);
        return await context.SaveChangesAsync()>0?
        "Pagamento removido com sucesso" :
        "Não foi possível remover o pagamento";
    }
}
