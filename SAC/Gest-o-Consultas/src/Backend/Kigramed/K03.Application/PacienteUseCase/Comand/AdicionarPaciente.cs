using System;
using System.Linq;
using Kigramed.K03.Application.PacienteUseCase.DTO;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;
using Kigramed.K02.Infra.Data;

namespace Kigramed.K03.Application.PacienteUseCase.Comand;

public class AdicionarPaciente
{
    private readonly IAdicionarRepository<PacienteModel> repository;
    private readonly KigramedDbContext context;

    public AdicionarPaciente(IAdicionarRepository<PacienteModel> repo, KigramedDbContext ctx)
    {
        repository = repo;
        context = ctx;
    }

    public async Task<string> ExecuteAsync(AdicionarPacienteDTO dto)
    {
        // Validar se cliente existe
        var clienteExiste = context.Tabelatb09_cliente.Any(c => c.Nif_cliente == dto.IdCliente);
        if (!clienteExiste)
            return $"Cliente com NIF '{dto.IdCliente}' não encontrado no sistema.";

        // Validar se gênero existe
        var generoExiste = context.Tabelatb10_genero.Any(g => g.Id == dto.IdGenero);
        if (!generoExiste)
            return $"Gênero com ID {dto.IdGenero} não encontrado.";

        // Validar se tipo de cliente existe
        var clientePacienteExiste = context.Tabelatb11_cliente_paciente.Any(cp => cp.Id == dto.IdCliente_Paciente);
        if (!clientePacienteExiste)
            return $"Tipo de paciente com ID {dto.IdCliente_Paciente} não encontrado.";

        var model = new PacienteModel
        {
            Nome = dto.PacienteNome,
            Data_nascimento = DateTime.SpecifyKind(dto.PacienteData_nascimento.Date, DateTimeKind.Utc),
            Id_genero = dto.IdGenero,
            Id_cliente = dto.IdCliente,
            Id_cliente_paciente = dto.IdCliente_Paciente
        };

        return await repository.AddAsync(model);
    }
}
