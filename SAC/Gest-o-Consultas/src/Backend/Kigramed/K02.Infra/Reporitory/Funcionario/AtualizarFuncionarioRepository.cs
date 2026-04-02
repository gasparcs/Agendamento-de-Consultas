using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Funcionario;

public class AtualizarFuncionarioRepository(KigramedDbContext context) : IActualizarRepository<FuncionarioModel>
{
    public async Task<string> ActualizarAsync(FuncionarioModel model)
    {
        var funcionario = await context.Tabelatb02_funcionario.FirstOrDefaultAsync(f=> f.Nif == model.Nif||f.Nome==model.Nome); 
        if(funcionario is null) return "Funcionário não encontrado";
        funcionario.Nome = model.Nome;
        funcionario.Perfil = model.Perfil;
         funcionario.Contactos = model.Contactos;
          funcionario.Estado = model.Estado;
         return await context.SaveChangesAsync() >0 ?
         "Atualização feita com sucesso" :
         "Não foi possível efectuar a atualização";        
        
    }
}
