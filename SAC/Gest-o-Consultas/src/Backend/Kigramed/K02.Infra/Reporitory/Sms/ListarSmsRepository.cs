using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D20.SMS;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Sms;

public class ListarSmsRepository(KigramedDbContext context): IListagemRepository<SMSModel>
{
     public async Task<IEnumerable<SMSModel>?> Listagem(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabelatb20_sms
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados;
        }
        catch ( Exception)
        {
            return [];
        }
    }
}
