using System;
using Kigramed.K03.Application.Perfil.DTO;
using Kigramed.K04.Domain.D01.Perfil;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.Perfil.Queries;

public class PegarPerfilPeloId(IPegarpeloId<PerfilModel> repository)
{
     public async Task<LeituraPerfilDTO?> ExecuteAsync(int id)
    {
        var perfil = await repository.PegarAsync(id);

        if(perfil is null) return null;

        return new LeituraPerfilDTO
        {
            PerfilDescricao = perfil.Descricao,

            Funcionarios = perfil.Funcionarios.Select( c => new FuncionarioDTO
            {
                FuncionarioNome = c.Nome,
                FuncionarioNif = c.Nif

            })

            
        };
    }
}
