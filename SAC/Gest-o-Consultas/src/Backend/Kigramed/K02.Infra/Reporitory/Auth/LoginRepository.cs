using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D05.Auth;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kigramed.K02.Infra.Reporitory.Auth;

public class LoginRepository(KigramedDbContext context) : ILoginRepository
{
    public async Task<string> LoginAsync(string nif, string senha)
    {
       var funcionario = await context.Tabelatb05_auth
       .FirstOrDefaultAsync(n=> n.Nif_funcionario ==nif);

       if(funcionario is null);

    return;
       

    }
}
