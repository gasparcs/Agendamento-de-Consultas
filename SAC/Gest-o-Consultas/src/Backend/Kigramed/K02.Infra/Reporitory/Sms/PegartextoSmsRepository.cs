using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D20.SMS;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Sms;

public class PegartextoSmsRepository(KigramedDbContext context): IPegarpeloTextoRepository<SMSModel>
{
    public async Task<IEnumerable<SMSModel>> PegarAsync(string texto, int pagina = 1, int quantidade = 20)
    {
        try
        {
             return await context.Tabelatb20_sms
                .Where(s => s.Mensagem.Contains(texto))
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();
    
        }
        catch (Exception)
        {
            return [];
        }
    }
}
