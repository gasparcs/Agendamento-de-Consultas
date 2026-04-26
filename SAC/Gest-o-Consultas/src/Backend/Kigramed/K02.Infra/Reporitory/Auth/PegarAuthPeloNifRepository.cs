using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D05.Auth;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Auth;

public class PegarAuthPeloNifRepository(KigramedDbContext context) : IPegarAuthPeloNifRepository
{
    public async Task<AuthModel?> PegarPeloNifAsync(string nif)
    {
        return await context.Tabelatb05_auth.FindAsync(nif);
    }
}