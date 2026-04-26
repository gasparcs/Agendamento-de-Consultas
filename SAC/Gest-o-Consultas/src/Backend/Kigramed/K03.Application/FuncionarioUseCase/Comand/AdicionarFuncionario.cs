using System;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Kigramed.K03.Application.Servico.IPasswordService;
using Kigramed.K03.Application.Servico.ISmsService;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D04.Contacto;
using Kigramed.K04.Domain.D07.MedicoEspecialidade;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.FuncionarioUseCase.Comand;

public class AdicionarFuncionario
(
IPegarpeloNifReporitory<FuncionarioModel> pesquisar,
IAdicionarRepository<FuncionarioModel> repository,
IPasswordCreate gerarSenha,
IPasswordHash criptoSenha,
ISmsService sms)
{
    public async Task<string> ExecuteAsync(AdicionarFuncionarioDTO dto)
    {
        var usuario = await pesquisar.PegarpeloNifAsync(dto.FuncionaioNif);

        if (usuario is not null) return ("Funcionario que pretende cadastrar já existe");

        string senha = await gerarSenha.GenerateAsync();

        var(senhaHash, saltHash) = await criptoSenha.HashAsync(senha);

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

        string texto = $"Caro sr(a) {model.Nome}, foste cadastrado na plataforma do predio azul, acesse o sistema no link: www.predioazul.com, use a seguinte credencial: Telefone : {model.Contactos} e Senha : {senha}";

        

        var smsResponse = await sms.EnviarAsync(model.Contactos,texto, "101010101010");
    }
}
