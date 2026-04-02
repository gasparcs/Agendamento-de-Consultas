using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Funcionario;

public class PegarnifFuncionarioRepository(KigramedDbContext context) : IPegarpeloNifReporitory<FuncionarioModel>
{
    public async Task<FuncionarioModel?> PegarpeloNifAsync(string nif)
    {
        return await context.Tabelatb02_funcionario.Include(n =>n.Perfil).Include(p =>p.Contactos).FirstOrDefaultAsync(n =>n.Nif ==nif);
         
    }
}


