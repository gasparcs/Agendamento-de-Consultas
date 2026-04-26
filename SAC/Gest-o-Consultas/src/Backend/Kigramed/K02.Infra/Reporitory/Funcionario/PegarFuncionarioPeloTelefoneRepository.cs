using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Funcionario;

public class PegarFuncionarioPeloTelefoneRepository(KigramedDbContext context) : IPegarFuncionarioPeloTelefoneRepository
{
    public async Task<FuncionarioModel?> PegarPeloTelefoneAsync(string telefone)
    {
        return await context.Tabelatb02_funcionario
            .Include(f => f.Perfil)
            .Include(f => f.Contactos)
            .ThenInclude(c => c.TipoContacto)
            .FirstOrDefaultAsync(f => f.Contactos.Any(c => c.Contacto == telefone && c.TipoContacto.Descricao.ToLower() == "telefone"));
    }
}