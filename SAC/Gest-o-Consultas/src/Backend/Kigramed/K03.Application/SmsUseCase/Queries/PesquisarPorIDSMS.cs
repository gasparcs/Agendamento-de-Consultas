using System;
using Kigramed.K03.Application.SmsUseCase.DTO;
using Kigramed.K04.Domain.D20.SMS;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.SmsUseCase.Queries;

public class PesquisarPorIDSMS(IPegarpeloId<SMSModel> repository)
{
    public async Task<SMSDTO?> ExecuteAsync(int id)
    {
        var sms = await repository.PegarAsync(id);

         if (sms is null) return null;

         return new SMSDTO
         {
             SMSId = sms.Id,

             SMSMensagem = sms.Mensagem,

             SMSEstado = sms.Estado,

             SMSData = sms.Data_envio,

             SMSNif_funcionario = sms.Nif_funcionario,

             SMSFuncionario = sms.Funcionario.Nome,

             SMSId_cliente = sms.Id_cliente,

             SMSCliente = sms.Cliente.Nome
         };
    }
}

