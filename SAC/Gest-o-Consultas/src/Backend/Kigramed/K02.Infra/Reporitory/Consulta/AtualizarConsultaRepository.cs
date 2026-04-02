using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Consulta;

public class AtualizarConsultaRepository(KigramedDbContext context) : IActualizarRepository<ConsultaModel>
{
    public async Task<string> ActualizarAsync(ConsultaModel model)
    {
        var consulta = await context.Tabelatb15_consulta
        .FirstOrDefaultAsync(c=>c.Id ==model.Id);
        if(consulta is null) return "Consulta não encontrada";
        consulta.MedicoConsulta=model.MedicoConsulta;
        consulta.EstadoConsulta=model.EstadoConsulta;
        consulta.Data_consulta=model.Data_consulta;
        return await context.SaveChangesAsync()>0?
        "Atualização feita com sucesso" :
        "Não foi possível efectuar a atualização";
    }
}
