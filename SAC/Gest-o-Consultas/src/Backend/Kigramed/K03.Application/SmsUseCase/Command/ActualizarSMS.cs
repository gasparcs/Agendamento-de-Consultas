using System;
using Kigramed.K03.Application.SmsUseCase.DTO;
using Kigramed.K04.Domain.D20.SMS;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.SmsUseCase.Command;

public class ActualizarSMS(IActualizarRepository<SMSModel> repository)
{
    public async Task<string> ExecuteAsync(AtualizarSMSDTO dto)
    {

        var model = new SMSModel
        {
            Id = dto.SMSId,
            Mensagem = dto.SMSMensagem,
            Estado = dto.SMSEstado,
            Nif_funcionario = dto.SMSNif_funcionario,
            Id_cliente = dto.SMSId_cliente
        };
          return await repository.ActualizarAsync(model);
    }
}

