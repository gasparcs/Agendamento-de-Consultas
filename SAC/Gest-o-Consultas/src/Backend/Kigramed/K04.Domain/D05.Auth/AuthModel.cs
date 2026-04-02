using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D02.Funcionario;

namespace Kigramed.K04.Domain.D05.Auth;
[Table("tb05_auth")]
public class AuthModel
{
    [Column("nif_funcionario")]
    public string Nif_funcionario { get; set; } = string.Empty;

    [Column("senha_hash")]
    public string Senha_hash { get; set; } = string.Empty;

    [Column("senha_salt")]
    public string Senha_Salt { get; set; } = string.Empty;

    public FuncionarioModel Funcionario { get; set; } = null!;
    

}
