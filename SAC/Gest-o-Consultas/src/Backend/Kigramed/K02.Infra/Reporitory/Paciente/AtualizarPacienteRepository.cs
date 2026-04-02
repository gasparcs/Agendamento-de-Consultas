using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Paciente;

public class AtualizarPacienteRepository(KigramedDbContext context) : IActualizarRepository<PacienteModel>
{
    public async Task<string> ActualizarAsync(PacienteModel model)
    {
        var paciente = await context.Tabelatb12_paciente.FirstOrDefaultAsync(p=> p.Id == model.Id||p.Nome==model.Nome);  
        if(paciente is null) return "Paciente não encontrado";
        paciente.Nome = model.Nome;
         return await context.SaveChangesAsync() > 0 ?
         "Atualização feita com sucesso." :
         "Não foi possível efectuar a atualização.";   
    }
}
