using System;
using Kigramed.K03.Application.EspecialidadeUseCase.DTO;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.EspecialidadeUseCase.Queries;

public class PegarEspecialidadePeloId(IPegarpeloId<EspecialidadeModel> repository) 
{
    public async Task<ListarEspecialidadeDTO ?> ExecuteAsync(int id)
    {
         var especialidade = await repository.PegarAsync(id);

         if (especialidade is null) return null;

         return new ListarEspecialidadeDTO
         {
             EspecialidadeId = especialidade.Id,

             EspecialidadeNome = especialidade.Nome,

             EspecialidadeDescricao = especialidade.Descricao,

             EspecialidadeEstado = especialidade.Estado,

             Servicos = especialidade.Servicos.Select( s =>  new ServicoDTO
             {
                ServicoDescricao = s.Nome      
             }),

             MedicoEspecialidade = especialidade.MedicoEspecialidades.Select( me =>  new MedicoEspecialidadeDTO
             {
                 FuncionarioNif = me.Nif_funcionario,

                  FuncionarioNome = me.Funcionario.Nome
             })
         };
    }
}
