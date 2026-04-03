using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Servico;

public class AdicionarServicoRepository(KigramedDbContext context) : IAdicionarRepository<ServicoModel>
{
    public async Task<string> AddAsync(ServicoModel model)
    {
        await context.Tabelatb08_servico.AddAsync(model);
        return await context.SaveChangesAsync()>0?
        "Serviço cadastrado com sucesso" :
        "Não foi posível cadastrar serviço";

    }
}
