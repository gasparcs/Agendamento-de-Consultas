using System;
using Kigramed.K03.Application.Servico.IPasswordService;

namespace Kigramed.K02.Infra.Servicos.PasswordService;

public class PasswordCreateService : IPasswordCreate
{
    public Task<string> GenerateAsync() =>

    Task.FromResult(new Random().NextInt64(100000, 999999).ToString());
}
