using System;
using Kigramed.K03.Application.PacienteUseCase.DTO;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.PacienteUseCase.Queries;

public class PegarPacientePeloTexto(IPegarpeloTextoRepository<PacienteModel> repository)
{
    public async Task<IEnumerable<LeituraPacienteDTO>?> ExecuteAsync(string texto)
    {
        var pacientes= await repository.PegarAsync(texto);

        if(pacientes is null) return null;

        return pacientes.Select(p=> new LeituraPacienteDTO
        {

            PacienteId= p.Id,

            PacienteNome= p.Nome,

            PacienteData_nascimento= p.Data_nascimento,

            Cliente_Paciente = p.ClientePaciente.Descricao,

            Cliente = p.Cliente.Nome,

            Genero = p.Genero.Nome,

            Consultas = p.Consultas.Select( c => new ConsultaDTO
            {
                IdConsuta = c.Id,

                Data_Consulta = c.Data_consulta
            })
    
        });
    }
}
