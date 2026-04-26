using System;

namespace Kigramed.K03.Application.Servico.ISmsService;

public interface ISmsService
{
    Task<bool> EnviarAsync(string telefone, string mensagemTexto, string nif);
}
