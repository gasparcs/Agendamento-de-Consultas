using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Reporitory.Servico;

public class AtualizarServicoRepository(KigramedDbContext context) : IActualizarRepository<ServicoModel>
{
    public async Task<string> ActualizarAsync(ServicoModel model)
    {
        var servico = await context.Tabelatb08_servico.FirstOrDefaultAsync(s=>s.Id == model.Id || s.Nome == model.Nome);
        if(servico is null) return "Serviço não encontrado";
        servico.Nome=model.Nome;
        servico.Duracao_minuto=model.Duracao_minuto;
        servico.Preco=model.Preco;
        servico.Estado=model.Estado;
        return await context.SaveChangesAsync()>0?
        "Actualização feita com sucesso" :
        "Não foi possível efectuar a actualização";
    }
}
