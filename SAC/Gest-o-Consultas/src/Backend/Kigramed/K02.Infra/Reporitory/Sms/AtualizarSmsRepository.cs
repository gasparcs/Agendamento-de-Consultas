using System;
using System.Runtime.CompilerServices;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Sms;

public class AtualizarSmsRepository(KigramedDbContext context): IActualizarRepository<>
{
      public async Task<SmsModel?> ActualizarAsync(int id, SmsModel model)
    {
        try
        {
            var sms = await context.TabelaSms.FindAsync(id);
            if (sms is null)
                return (null, "SMS nao encontrado", 404);

            sms.Mensagem = model.Mensagem;
            sms.Estado = model.Estado;
            sms.Data = model.Data;
            sms.IdFuncionario = model.IdFuncionario;
            sms.IdInquilino = model.IdInquilino;

            return await context.SaveChangesAsync() > 0 ?
                (sms, "SMS actualizado com sucesso", 200) :
                (null, "Nao foi possivel actualizar o SMS", 500);
        }
        catch (DbUpdateException ex)
        {
            return ( ex.ToString());
        }
    }
}
