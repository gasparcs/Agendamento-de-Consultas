using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D05.Auth;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Auth;

public class PegarAuthPeloNifRepository(KigramedDbContext context) : IPegarAuthPeloNifRepository
{
    public async Task<AuthModel?> PegarPeloNifAsync(string nif)
    {
        return await context.Tabelatb05_auth
            .Include(a => a.Funcionario)
                .ThenInclude(f => f.Perfil)
            .Include(a => a.Funcionario)
                .ThenInclude(f => f.Contactos)
                    .ThenInclude(c => c.TipoContacto)
            .FirstOrDefaultAsync(a => a.Nif_funcionario == nif);
    }
}