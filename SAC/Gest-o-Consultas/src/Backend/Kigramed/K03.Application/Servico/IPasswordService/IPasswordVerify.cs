using System;

namespace Kigramed.K03.Application.Servico.IPasswordService;

public class IPasswordVerify
{
        Task<bool> VerifyAsync(string senha, string hashArmazenado, string saltArmazenado);

}
