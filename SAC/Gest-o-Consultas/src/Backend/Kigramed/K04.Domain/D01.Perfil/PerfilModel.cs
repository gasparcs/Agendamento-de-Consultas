using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D16.PerfilPermissao;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kigramed.K04.Domain.D01.Perfil;

[Table("tb01_perfil")]
public class PerfilModel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    public ICollection<FuncionarioModel> Funcionarios { get; set; } =[];

    public ICollection<PerfilPermissaoModel> PerfisPermissoes { get; set; } = [];
    
    }
