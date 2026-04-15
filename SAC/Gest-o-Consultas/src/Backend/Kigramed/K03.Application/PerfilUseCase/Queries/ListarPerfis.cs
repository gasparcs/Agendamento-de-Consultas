using System;
using Kigramed.K03.Application.PerfilUseCase.DTO;
using Kigramed.K04.Domain.D01.Perfil;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.PerfilUseCase.Queries;

public class ListarPerfis(IListagemRepository<PerfilModel> repository)
{
    public async Task<IEnumerable<LeituraPerfilDTO>> ExecuteAsync()
    {
        var perfis = await repository.Listagem();

        return perfis.Select(p => new LeituraPerfilDTO
            {
                PerfilId = p.Id,

                PerfilDescricao = p.Descricao,

                Funcionarios = p.Funcionarios.Select( f => new FuncionarioDTO
                    {
                        Nome = f.Nome
                    }),
                
                PerfilPermissoes = p.PerfisPermissoes.Select( pp => new PerfilPermissaoDTO
                    {
                        Permissao = new PermissaoDTO
                        {
                            Descricao = pp.Permissao.Descricao
                        }
                    })
            
        });
    }
}
