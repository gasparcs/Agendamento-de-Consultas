using System;
using System.Runtime.CompilerServices;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D20.SMS;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Sms;

public class AtualizarSmsRepository(KigramedDbContext context): IActualizarRepository<SMSModel>
{
      public async Task<string> ActualizarAsync( SMSModel model)
    {
        try
        {
            var sms = await context.Tabelatb20_sms.FirstOrDefaultAsync(s=>s.Id == model.Id);
            if (sms is null) return "SMS não encontrada";

            sms.Mensagem = model.Mensagem;
            sms.Estado = model.Estado;
            sms.Data_envio = model.Data_envio;
            sms.Nif_funcionario = model.Nif_funcionario;
            sms.Id_cliente = model.Id_cliente;

            return await context.SaveChangesAsync() > 0 ?
                "SMS actualizado com sucesso" :
                "Nao foi possivel actualizar o SMS";
        }
        catch (DbUpdateException ex)
        {
            return ex.ToString();
        }
    }
}
