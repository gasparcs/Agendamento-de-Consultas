using System;

namespace Kigramed.K03.Application.Servico.ITokenService;

public class ITokenService
{
    string GerarToken(int usuarioId, string nome, string telefone, string role, string tipoUsuario);
}
