using System;
using Kigramed.K03.Application.Servico.ITokenService;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens; 


namespace Kigramed.K02.Infra.Servicos.AuthServico;

public class JwtTokenService(IConfiguration configuration) : ITokenService
{
   public string GerarToken(string nif, string nome, string telefone, string role, string perfil)
    {
        string chave = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key não configurada.");

        string issuer = configuration["Jwt:Issuer"] ?? "Kigramed.API";

        string audience = configuration["Jwt:Audience"] ?? "Kigramed.Client";
        
        int expiraMinutos = 120;

        string? expiraConfig = configuration["Jwt:ExpiryMinutes"];

        if (!string.IsNullOrWhiteSpace(expiraConfig) && int.TryParse(expiraConfig, out int valor))
        
        expiraMinutos = valor;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, nif),

            new(JwtRegisteredClaimNames.UniqueName, nome),

            new("telefone", telefone),

            new(ClaimTypes.Role, role),         // "Funcionario" ou "Cliente"

            new("perfil", perfil)      // ex: "Medico", "Recepcionista"
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(

            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiraMinutos),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
