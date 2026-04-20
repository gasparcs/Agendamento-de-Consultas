using System;
using Kigramed.K03.Application.EspecialidadeUseCase.DTO;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.EspecialidadeUseCase.Queries;

public class PegarEspecialidadePeloTexto(IPegarpeloTextoRepository<EspecialidadeModel> repository)
{
    public async Task<IEnumerable<ListarEspecialidadeDTO>?> ExecuteAsync(string texto)
    {
        var especialidades = await repository.PegarAsync(texto);

        if (especialidades is null) return null;

        return especialidades.Select( e => new ListarEspecialidadeDTO
        {
           EspecialidadeId = e.Id,

           EspecialidadeNome = e.Nome,

           EspecialidadeDescricao = e.Descricao,

           EspecialidadeEstado = e.Estado,

           Servicos = e.Servicos.Select( s =>  new ServicoDTO 
            {
                ServicoDescricao = s.Nome
            }), 

            MedicoEspecialidade = e.MedicoEspecialidades.Select( me =>  new MedicoEspecialidadeDTO
            {
                FuncionarioNif = me.Nif_funcionario
            })
        });
    }
}
