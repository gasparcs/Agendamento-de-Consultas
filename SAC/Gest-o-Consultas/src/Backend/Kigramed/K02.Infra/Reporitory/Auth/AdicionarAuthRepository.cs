using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D05.Auth;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Auth;

public class AdicionarAuthRepository(KigramedDbContext context) : IAdicionarRepository<AuthModel>
{
    public async Task<string> AddAsync(AuthModel model)
    {
       await context.Tabelatb05_auth.AddAsync(model);
       return await context.SaveChangesAsync()>0?
       "Credenciais adicionadas com sucesso" :
       "Não foi possível adicionar credenciais"; 

    }
}
