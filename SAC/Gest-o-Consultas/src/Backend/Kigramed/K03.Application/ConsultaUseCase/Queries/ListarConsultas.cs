using System;
using Kigramed.K03.Application.ConsultaUseCase.DTO;
using Kigramed.K04.Domain.D15.Consulta;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ConsultaUseCase.Queries;

public class ListarConsultas(IListagemRepository<ConsultaModel> repository)
{
     public async Task<IEnumerable<ListarConsulta>> ExecuteAsync()
    {
        var consultas = await repository.Listagem();

        return consultas.Select( c => new ListarConsulta
        {
           IdConsulta = c.Id,

           IdMedicoEspecialidade = c.MedicoEspecialidade.Funcionario.Nome,

           IdPaciente = c.Paciente.Nome,

           Cliente = c.Paciente.Cliente.Nome,

           IdEstado = c.EstadoConsulta.Descricao,

            Servicos = c.Servico.Nome,

            DataConsulta = c.Data_consulta

        });
    }
}       
