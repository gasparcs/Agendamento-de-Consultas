using System;
using Kigramed.K03.Application.SmsUseCase.DTO;
using Kigramed.K04.Domain.D20.SMS;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.SmsUseCase.Queries;

public class PesquisarTodasSMS(IListagemRepository<SMSModel> repository)
{
    public async Task<IEnumerable<SMSDTO>> ExecuteAsync()
    {
        var sms = await repository.Listagem();
        return sms.Select(s => new SMSDTO
        {
            SMSId = s.Id,
            SMSMensagem = s.Mensagem,
            SMSEstado = s.Estado,
            SMSData = s.Data_envio,
            SMSNif_funcionario = s.Nif_funcionario,
            SMSFuncionario = s.Funcionario.Nome,
            SMSId_cliente = s.Id_cliente,
            SMSCliente = s.Cliente.Nome
        });
    }
}


