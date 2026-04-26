using System;

namespace Kigramed.K03.Application.Servico.ITokenService;

public interface ITokenService
{
    string GerarToken(string nif, string nome, string telefone, string role, string perfil);

}
