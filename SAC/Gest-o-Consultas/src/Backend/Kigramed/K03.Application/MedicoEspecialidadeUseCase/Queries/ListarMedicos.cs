using System;
using Kigramed.K03.Application.MedicoEspecialidadeUseCase.DTO;
using Kigramed.K04.Domain.D07.MedicoEspecialidade;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.MedicoEspecialidadeUseCase.Queries;

public class ListarMedicos(IListagemRepository<MedicoEspecilidadeModel> repository)
{
     public async Task<IEnumerable<LeituraMedicoEspecialidade>> ExecuteAsync()
    {
        var medicos = await repository.Listagem();

        return medicos.Select(p => new LeituraMedicoEspecialidade
            {
                Id = p.Id,

                Nomefuncionario = p.Funcionario.Nome,

                NomeEspecialidade = p.Especialidade.Nome
            
        });
    }
}
