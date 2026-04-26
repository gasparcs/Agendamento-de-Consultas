using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D20.SMS;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Sms;

public class PegaridSmsRepository(KigramedDbContext context): IPegarpeloId<SMSModel>
{
      public async Task<SMSModel?> PegarAsync(int id)
    {
        try
        {
           return await context.Tabelatb20_sms
           .FirstAsync(s=>s.Id == id);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
