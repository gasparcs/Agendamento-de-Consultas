using System;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.FuncionarioUseCase.Queries;

public class ListarFuncionarios(IListagemRepository<FuncionarioModel> repository)
{ 
    public async Task<IEnumerable<ListrarFuncionarioDTO>> ExecuteAsync()
    {
        var funcionarios = await repository.Listagem();

        return funcionarios.Select( f => new ListrarFuncionarioDTO

        {
            FuncionarioNif = f.Nif,

            FUncionarioPerfil = new PerfilDTO
            {
                Descricao = f.Perfil.Descricao
            },

            FuncionarioNome = f.Nome,

            FuncionaroEstado = f.Estado,

            Contactos = f.Contactos.Select( fc => new ContactoDTO
            {
                TipoContacto = new TipoContactoDTO
                {
                    Descricao = fc.TipoContacto.Descricao 
                },

                Contacto = fc.Contacto
            }),

        });
    }
}
