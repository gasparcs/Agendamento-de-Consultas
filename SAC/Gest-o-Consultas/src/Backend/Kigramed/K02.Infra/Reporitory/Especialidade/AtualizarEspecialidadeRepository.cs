using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Especialidade;

public class AtualizarEspecialidadeRepository(KigramedDbContext context) : IActualizarRepository<EspecialidadeModel>
{
    public async Task<string> ActualizarAsync(EspecialidadeModel model)
    {
        var especialidade = await context.Tabelatb06_especialidade.FirstOrDefaultAsync(f=> f.Id == model.Id||f.Nome==model.Nome); 
        if(especialidade is null) return "Especialidade não encontrada";
        especialidade.Nome = model.Nome;
        especialidade.Descricao = model.Descricao;
          especialidade.Estado = model.Estado;
         return await context.SaveChangesAsync() >0 ?
         "Atualização feita com sucesso" :
         "Não foi possível efectuar a atualização";       
    }
}
