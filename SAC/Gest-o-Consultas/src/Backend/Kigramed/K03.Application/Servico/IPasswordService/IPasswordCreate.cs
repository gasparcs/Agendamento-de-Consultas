using System;

namespace Kigramed.K03.Application.Servico.IPasswordService;

public interface IPasswordCreate
{
       Task<string> GenerateAsync();
}
