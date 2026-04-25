using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Sms;

public class PegartextoSmsRepository(KigramedDbContext context): IPegarpeloTextoRepository< model>
{
    public async Task<IEnumerable<SmsModel>> PesquisarPorTextoAsync(string texto, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.TabelaSms
                .Where(s => s.Mensagem.Contains(texto))
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "SMS encontrados com sucesso", 200) :
                ([], "Nenhum SMS encontrado", 404);
        }
        catch (Exception ex)
        {
            return ([], ex.ToString(), 500);
        }
    }
}
