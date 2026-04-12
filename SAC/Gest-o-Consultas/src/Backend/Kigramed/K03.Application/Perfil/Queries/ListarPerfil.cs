using System;
using Kigramed.K03.Application.Perfil.DTO;
using Kigramed.K04.Domain.D01.Perfil;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.Perfil.Queries;

public class ListarPerfil(IListagemRepository<PerfilModel> repository)
{
     public async Task<IEnumerable<LeituraPerfilDTO>> ExecuteAsync()
    {
         var perfil = await repository.Listagem();

        return perfil.Select(c => new LeituraPerfilDTO
        {
            PerfilDescricao = c.Descricao,


            Funcionarios = c.Funcionarios.Select( ct => new FuncionarioDTO
                {
                    FuncionarioNif = ct.Nif,
                    FuncionarioNome = ct.Nome
                })
        });        
    }
        
}
