using System;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D04.Contacto;
using Kigramed.K04.Domain.D07.MedicoEspecialidade;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.FuncionarioUseCase.Comand;

public class AdicionarFuncionario(IAdicionarRepository<FuncionarioModel> repository)
{
    public async Task<string> ExecuteAsync(AdicionarFuncionarioDTO dto)
    {
        var model = new FuncionarioModel
        {
            Nif = dto.FuncionaioNif,

            Id_Perfil = dto.FuncionarioPerfil,

            Nome = dto.FuncionarioNome,

            Estado = dto.FuncionarioEstado,

            MedicoEspecialidades= dto.Especialidades.Select(e => new MedicoEspecilidadeModel
            {
                Id_especialidade= e.IdEspecialidade,

                Nif_funcionario = dto.FuncionaioNif
                
            }).ToList(),

            Contactos = dto.Contactos.Select( c => new ContactoModel
            {
                Id_tipo_contacto = c.TipoContacto,

                Contacto = c.Contacto,

                Nif_funcionario = dto.FuncionaioNif,

                Nif_cliente = null
            }).ToList()
        };

        return await repository.AddAsync(model);
    }
}
