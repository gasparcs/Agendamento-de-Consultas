using System;

namespace Kigramed.K03.Application.Servico.ISmsService;

public class IsmsService
{
    Task<bool> EnviarAsync(string telefone, string mensagemTexto, string nif);
}
