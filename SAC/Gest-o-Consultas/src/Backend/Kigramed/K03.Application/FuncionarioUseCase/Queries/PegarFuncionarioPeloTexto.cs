using System;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.FuncionarioUseCase.Queries;

public class PegarFuncionarioPeloTexto(IPegarpeloTextoRepository<FuncionarioModel> repository)
{
    public async Task<IEnumerable<ListrarFuncionarioDTO>?> ExecuteAsync(string texto)
    {
        var funcionario = await repository.PegarAsync(texto);

        if(funcionario is null) return null!;

        return funcionario.Select( f => new ListrarFuncionarioDTO
        {
           FuncionarioNif = f.Nif,

           FuncionarioNome = f.Nome, 

         FUncionarioPerfil = f.Perfil.Descricao,

        FuncionaroEstado = f.Estado,

        Contactos = f.Contactos.Select( f => new ContactoDTO
        {
            Contacto = f.Contacto
        })

        });
    }
}
