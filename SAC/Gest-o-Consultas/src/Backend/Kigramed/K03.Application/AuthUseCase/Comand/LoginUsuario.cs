using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Kigramed.K03.Application.AuthUseCase.DTO;
using Kigramed.K03.Application.Servico.IPasswordService;
using Kigramed.K03.Application.Servico.ITokenService;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D05.Auth;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.AuthUseCase.Comand;

public class LoginUsuario(
    IPegarAuthPeloNifRepository authRepository,
    IPasswordVerify passwordVerify,
    ITokenService tokenService)
{
    public async Task<LoginResponseDTO?> ExecuteAsync(LoginRequestDTO request)
    {
        // Buscar dados de autenticação pelo NIF
        var auth = await authRepository.PegarPeloNifAsync(request.Nif);
        if (auth == null)
        {
            return null; // Usuário não encontrado
        }

        // Verificar senha
        bool senhaValida = await passwordVerify.VerifyAsync(request.Senha, auth.Senha_hash, auth.Senha_Salt);
        if (!senhaValida)
        {
            return null; // Senha inválida
        }

        // Normalizar o perfil para role
        string normalizedRole = NormalizarPerfil(auth.Funcionario.Perfil?.Descricao);

        // Obter telefone
        string telefone = auth.Funcionario.Contactos
            .FirstOrDefault(c => c.TipoContacto.Descricao.ToLower() == "telefone")?
            .Contacto ?? string.Empty;

        // Gerar token
        string token = tokenService.GerarToken(
            nif: auth.Nif_funcionario,
            nome: auth.Funcionario.Nome,
            telefone: telefone,
            role: normalizedRole,
            perfil: auth.Funcionario.Perfil?.Descricao ?? "Sem Perfil"
        );

        // Retornar resposta
        return new LoginResponseDTO
        {
            Nif = auth.Nif_funcionario,
            Nome = auth.Funcionario.Nome,
            Perfil = auth.Funcionario.Perfil?.Descricao ?? "Sem Perfil",
            telefone = telefone,
            Token = token,
            role = normalizedRole
        };
    }

    private static string NormalizarPerfil(string? descricaoPerfil)
    {
        if (string.IsNullOrWhiteSpace(descricaoPerfil))
            return "Funcionario";

        string semAcentos = RemoverAcentos(descricaoPerfil).Trim().ToLowerInvariant();

        if (semAcentos.Contains("admin"))
            return "Admin";

        if (semAcentos.Contains("medico") || semAcentos.Contains("médico"))
            return "Medico";

        if (semAcentos.Contains("secretaria") || semAcentos.Contains("secretário"))
            return "Secretaria";

        return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(semAcentos);
    }

    private static string RemoverAcentos(string texto)
    {
        string normalized = texto.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder();

        foreach (char c in normalized)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                builder.Append(c);
        }

        return builder.ToString().Normalize(NormalizationForm.FormC);
    }
}

