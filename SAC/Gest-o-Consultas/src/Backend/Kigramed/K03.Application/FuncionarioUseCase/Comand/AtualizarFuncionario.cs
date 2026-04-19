using System;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D04.Contacto;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.FuncionarioUseCase.Comand;
 
public class AtualizarFuncionario(IActualizarRepository<FuncionarioModel> repository)
{
    public async Task<string> ExecuteAsync(AtualizarFuncionarioDTO dto)
    {
        var model = new FuncionarioModel
        {
            Nif = dto.FuncionaioNif,

            Id_Perfil = dto.FuncionarioPerfil,

            Nome = dto.FuncionarioNome,

            Estado = dto.FuncionarioEstado,

            Contactos= dto.Contactos.Select(c => new ContactoModel
            {
                Id_tipo_contacto = c.TipoContacto,

                Contacto = c.Contacto

            }).ToList() 
        };

        return await repository.ActualizarAsync(model);
    }
}
