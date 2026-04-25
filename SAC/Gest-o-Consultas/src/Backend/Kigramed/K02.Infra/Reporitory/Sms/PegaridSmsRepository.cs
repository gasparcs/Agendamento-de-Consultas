using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Sms;

public class PegaridSmsRepository(KigramedDbContext context): IPegarpeloId<>
{
      public async Task<SmsModel?> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var sms = await context.TabelaSms.FindAsync(id);
            return sms is not null ?
                (sms, "SMS encontrado com sucesso") :
                (null, "SMS nao encontrado");
        }
        catch (Exception ex)
        {
            return ( ex.ToString());
        }
    }
}
