using System;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.FuncionarioUseCase.Queries;

public class PegarFuncionarioPeloNif(IPegarpeloNifReporitory<FuncionarioModel> repository)
{
    public async Task<ListrarFuncionarioDTO> ExecuteAsync(string nif) 
    {
        var funcionario = await repository.PegarpeloNifAsync(nif);

        if (funcionario is null) return null!;

        return new ListrarFuncionarioDTO
        {
            FuncionarioNif = funcionario.Nif,

            FuncionarioNome = funcionario.Nome,

            FUncionarioPerfil = funcionario.Perfil.Descricao,

            FuncionaroEstado = funcionario.Estado,

            Contactos = funcionario.Contactos.Select( f => new ContactoDTO
            {
                Contacto = f.Contacto
            })
        };


    }
}
