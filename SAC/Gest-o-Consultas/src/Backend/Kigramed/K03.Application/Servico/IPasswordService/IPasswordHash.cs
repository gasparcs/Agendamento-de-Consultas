using System;

namespace Kigramed.K03.Application.Servico.IPasswordService;

public interface IPasswordHash
{
     Task<(string Hash, string Salt)> HashAsync(string senha);
}
