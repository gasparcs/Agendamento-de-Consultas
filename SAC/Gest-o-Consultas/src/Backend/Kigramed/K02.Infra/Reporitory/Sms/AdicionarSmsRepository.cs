using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.D20.SMS;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Sms;

public class AdicionarSmsRepository(KigramedDbContext context) : IAdicionarRepository<SMSModel>
{
   public async Task<string> AddAsync( SMSModel model)
    {
        try
        {
           await context.Tabelatb20_sms.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                "SMS cadastrada com sucesso" :
                "Nao foi possivel cadastrar a SMS";
        }
        catch (DbUpdateException ex)
        {
            return ex.ToString();
        }
    }
}
