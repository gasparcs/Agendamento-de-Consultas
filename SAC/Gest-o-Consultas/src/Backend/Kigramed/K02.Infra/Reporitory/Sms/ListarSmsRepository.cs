using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Sms;

public class ListarSmsRepository(KigramedDbContext context): IListagemRepository<>
{
     public async Task<IEnumerable<SmsModel?>> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.TabelaSms
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "SMS pesquisados com sucesso", 200) :
                ([], "Nenhum SMS encontrado", 404);
        }
        catch (Exception ex)
        {
            return ( ex.ToString());
        }
    }
}
