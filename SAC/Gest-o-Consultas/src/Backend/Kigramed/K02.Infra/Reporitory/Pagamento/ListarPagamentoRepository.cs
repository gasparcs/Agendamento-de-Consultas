using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Pagamento;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Pagamento;

public class ListarPagamentoRepository(KigramedDbContext context) : IListagemRepository<PagamentoModel>
{
    public async Task<IEnumerable<PagamentoModel>> Listagem(int pagina = 1, int quantidade = 20)
    {
        var pagamentos = await context.Tabelatb14_pagamento
        .Include(c => c.Cliente)
        .Include(f => f.Funcionario)
        .Include(c => c.Comprovativo)
        .Include(e => e.Data_envio)
        .Include(p => p.PagamentoConsultas)
        .Skip((pagina - 1) * quantidade)
        .Take(quantidade)
        .ToListAsync();
        return pagamentos;
    }
}
