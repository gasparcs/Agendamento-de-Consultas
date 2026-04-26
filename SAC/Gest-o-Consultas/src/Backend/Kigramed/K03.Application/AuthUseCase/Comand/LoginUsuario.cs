using System;
using Kigramed.K03.Application.AuthUseCase.DTO;
using Kigramed.K03.Application.Servico.IPasswordService;
using Kigramed.K03.Application.Servico.ITokenService;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.AuthUseCase.Comand;

public class LoginUsuario(
    IPegarpeloNifReporitory<FuncionarioModel> pegarpelonif,
    IPegarpeloId<ClienteModel> pegarpeloid,
     IPasswordVerify passwordVerify,
    ITokenService tokenService)

{

    public async Task<(LoginResponseDTO? dados, string mensagem, int codigo)> Executar(LoginRequestDTO dto)
    {
        var funcionario = await pegarpelonif.PegarpeloNifAsync(dto.Telefone);
        if (funcionario is not null)
        {
            if (!funcionario.Estado)
                return (null, "Usuario desativado.", 403);

            bool senhaOk = await passwordVerify.VerifyAsync(dto.Senha, funcionario.Senha, funcionario.Salt);
            if (!senhaOk)
                return (null, "Credenciais invalidas.", 401);

            var (perfil, _, _) = await pesquisarPerfil.PesquisarPorIdAsync(funcionario.IdPerfil);
            string role = NormalizarPerfil(perfil?.Descricao);

            string token = tokenService.GerarToken(funcionario.Id, funcionario.Nome, funcionario.Telefone, role, "Funcionario");
            var response = new LoginResponseDTO
            {
                UsuarioId = funcionario.Id,
                Nome = funcionario.Nome,
                Telefone = funcionario.Telefone,
                Perfil = role,
                TipoUsuario = "Funcionario",
                Token = token
            };

            return (response, "Login realizado com sucesso.", 200);
        }

        var inquilino = await pegarpeloid.PegarAsync(dto.Telefone);
        if (inquilino is null)
            return (null, "Credenciais invalidas.", 401);

        if (!inquilino.Estado)
            return (null, "Usuario desativado.", 403);

        bool senhaValida = await passwordVerify.VerifyAsync(dto.Senha, inquilino.Senha, inquilino.Salt);
        if (!senhaValida)
            return (null, "Credenciais invalidas.", 401);

        string tokenInquilino = tokenService.GerarToken(inquilino.Id, inquilino.Nome, inquilino.Telefone, "Inquilino", "Inquilino");
        var respostaInquilino = new LoginResponseDTO
        {
            UsuarioId = inquilino.Id,
            Nome = inquilino.Nome,
            Telefone = inquilino.Telefone,
            Perfil = "Inquilino",
            TipoUsuario = "Inquilino",
            Token = tokenInquilino
        };

        return (respostaInquilino, "Login realizado com sucesso.", 200);
    }

    private static string NormalizarPerfil(string? descricaoPerfil)
    {
        if (string.IsNullOrWhiteSpace(descricaoPerfil))
            return "Funcionario";

        string semAcentos = RemoverAcentos(descricaoPerfil).Trim().ToLowerInvariant();

        if (semAcentos.Contains("admin"))
            return "Admin";

        if (semAcentos.Contains("sind"))
            return "Sindico";

        if (semAcentos.Contains("inquilino"))
            return "Inquilino";

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

