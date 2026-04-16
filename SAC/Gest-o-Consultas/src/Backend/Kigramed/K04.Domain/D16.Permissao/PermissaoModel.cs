using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D16.PerfilPermissao;

namespace Kigramed.K04.Domain.D15.Permissao;
[Table("tb16_permissoes")]
public class PermissaoModel
{
    [Key]
    [Column("uuid_permissoes")]
    public Guid UUID { get; set; }

    [Column("descricao")]
    public string Descricao{ get; set; } = string.Empty;

    public ICollection<PerfilPermissaoModel> PerfisPermissoes { get; set; } = [];

}
