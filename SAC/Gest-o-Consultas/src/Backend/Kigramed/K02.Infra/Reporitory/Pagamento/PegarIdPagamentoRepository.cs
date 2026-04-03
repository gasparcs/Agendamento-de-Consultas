using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Pagamento;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Pagamento;

public class PegarIdPagamentoRepository(KigramedDbContext context) : IPegarpeloId<PagamentoModel>
{
    public async Task<PagamentoModel?> PegarAsync(int id)
    {
        return await context.Tabelatb14_pagamento
        .Include(c=>c.Cliente)
        .Include(f=>f.Funcionario)
        .Include(c=>c.Comprovativo)
        .Include(e=>e.Data_envio)
        .FirstAsync(p=>p.Id==id);
    }
}
