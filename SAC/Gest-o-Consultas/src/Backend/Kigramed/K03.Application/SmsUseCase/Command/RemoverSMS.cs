using System;
using Kigramed.K04.Domain.D20.SMS;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.SmsUseCase.Command;

public class RemoverSMS(IRemoverRepository<SMSModel> repository)
{
  public async Task<string> ExecuteAsync(int id)
    {
         var model = new SMSModel
        { 
             Id = id
        };

        return await repository.RemoverAsync(model);
    }
 
}
