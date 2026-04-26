using System;
using Kigramed.K03.Application.SmsUseCase.DTO;
using Kigramed.K04.Domain.D20.SMS;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.SmsUseCase.Command;

public class CadastrarSms(IAdicionarRepository<SMSModel> repository)
{
    public async Task<string> ExecuteAsync(CadastrarSMSDTO dto)
    {
        var model = new SMSModel
        {
            Mensagem = dto.SMSMensagem,
            Nif_funcionario = dto.SMSNif_funcionario,
            Id_cliente = dto.SMSId_cliente,
        };

            return await repository.AddAsync(model);
    }
}
