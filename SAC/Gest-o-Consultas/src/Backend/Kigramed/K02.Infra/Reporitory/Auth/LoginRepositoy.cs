using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D05.Auth;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Auth;

public class LoginRepositoy(KigramedDbContext context) : ILoginRepository
{
    public async Task<string> LoginAsync(string nif, string senha)
    {
        var usuario= await context.Tabelatb05_auth.FirstOrDefaultAsync(n=>n.Nif_funcionario==nif);

        if(usuario is null) return "Usuário não encontrado";
        
        if(usuario.Senha_Salt != senha)
        {
         return "Senha incorreta";
        } 
       return "Login realizado com sucesso";
    }
}
