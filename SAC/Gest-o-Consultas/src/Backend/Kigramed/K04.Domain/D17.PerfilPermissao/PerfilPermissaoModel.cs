using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D01.Perfil;
using Kigramed.K04.Domain.D15.Permissao;

namespace Kigramed.K04.Domain.D16.PerfilPermissao;
[Table("tb17_perfil_permissoes")]
public class PerfilPermissaoModel
{
[Key]
[Column("id")]
public int Id { get; set; }

[Column("uuid_permissoes")]
public Guid UUID_permissao { get; set; }

[Column("id_perfil")]
public int Id_perfil { get; set; }

public PermissaoModel Permissao { get; set; } = null!;

public PerfilModel Perfil { get; set; } = null!;
}
