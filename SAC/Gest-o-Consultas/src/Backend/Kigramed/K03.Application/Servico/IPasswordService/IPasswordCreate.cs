using System;

namespace Kigramed.K03.Application.Servico.IPasswordService;

public class IPasswordCreate
{
   Task<string> GenerateAsync();
}
