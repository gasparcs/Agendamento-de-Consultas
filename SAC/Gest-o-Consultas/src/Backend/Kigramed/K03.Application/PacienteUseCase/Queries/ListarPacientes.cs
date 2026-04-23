using System;
using Kigramed.K03.Application.PacienteUseCase.DTO;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.PacienteUseCase.Queries;

public class ListarPacientes(IListagemRepository<PacienteModel> repository)
{
   public async Task<IEnumerable<LeituraPacienteDTO>> ExecuteAsync()
    {
        var pacientes= await repository.Listagem();

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

                Data_Consulta = c.Data_consulta,

                Estado = c.EstadoConsulta.Descricao
            })
    
        });
    }
}
