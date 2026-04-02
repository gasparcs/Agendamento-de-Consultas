using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Funcionario;

public class ListarFuncionarioRepository(KigramedDbContext context) : IListagemRepository<FuncionarioModel>
{
    public async Task<IEnumerable<FuncionarioModel>> Listagem()
    {
        var funcionarios =await context.Tabelatb02_funcionario.Include(n =>n.Perfil).Include(c =>c.Contactos).ToListAsync();
        return funcionarios;
    }
}
