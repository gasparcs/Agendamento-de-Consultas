using System;

namespace Kigramed.K03.Application.Servico.IPasswordService;

public class IPasswordHash
{
     Task<(string Hash, string Salt)> HashAsync(string senha);
}
