using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Funcionario;

public class ListarFuncionarioRepository(KigramedDbContext context) : IListagemRepository<FuncionarioModel>
{
    public async Task<IEnumerable<FuncionarioModel>> Listagem(int pagina = 1, int quantidade = 20)
    {
        var funcionarios = await context.Tabelatb02_funcionario.Include(n => n.Perfil).Include(c => c.Contactos).ThenInclude(c => c.TipoContacto)
        .Skip((pagina - 1) * quantidade)
        .Take(quantidade)
        .ToListAsync();
        return funcionarios;
    }
}
