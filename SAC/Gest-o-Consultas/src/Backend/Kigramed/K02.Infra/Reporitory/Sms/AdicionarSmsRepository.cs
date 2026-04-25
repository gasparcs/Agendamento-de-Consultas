using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Sms;

public class AdicionarSmsRepository(KigramedDbContext context): IAdicionarRepository< model>
{
   public async Task<>  AddyAsync(model)
    {
        try
        {
           await context.tb20_sms.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "SMS cadastrado com sucesso") :
                (null, "Nao foi possivel cadastrar o SMS");
        }
        catch (DbUpdateException ex)
        {
            
            return (ex.ToString());
        }
    }
}
