using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D16.PerfilPermissao;

namespace Kigramed.K04.Domain.D15.Permissao;
[Table("tb15_permissoes")]
public class PermissaoModel
{
    [Column("uuid")]
    public Guid UUID { get; set; }

    [Column("descricao")]
    public string Descricao{ get; set; } = string.Empty;

    public ICollection<PerfilPermissaoModel> PerfisPermissoes { get; set; } = [];

}
