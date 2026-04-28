using System;
using Kigramed.K02.Infra.Data;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K02.Infra.Reporitory.Paciente;

public class AdicionarPacienteRepository(KigramedDbContext context) : IAdicionarRepository<PacienteModel>
{
    public async Task<string> AddAsync(PacienteModel model)
    {
        try
        {
            await context.Tabelatb12_paciente.AddAsync(model);
            var result = await context.SaveChangesAsync();
            Console.WriteLine($"[DEBUG] Paciente adicionado: Nome={model.Nome}, IdCliente={model.Id_cliente}, IdGenero={model.Id_genero}, IdClientePaciente={model.Id_cliente_paciente}, SaveResult={result}");
            return result > 0 ?
            "Paciente cadastrado com sucesso." :
            "Não foi possível cadastrar o paciente.";
        }
        catch (Exception ex)
        {
            var rootMessage = ex.GetBaseException().Message;
            Console.WriteLine($"[ERROR] Erro ao adicionar paciente: {ex.Message}. Inner: {ex.InnerException?.Message}");
            return $"Erro ao cadastrar paciente: {rootMessage}";
        }
    }
}
