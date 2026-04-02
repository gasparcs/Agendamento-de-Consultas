using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D14.Consulta;

namespace Kigramed.K04.Domain.D13.EstadoConsulta;
[Table("tb13_estado_consulta")]
public class EstadoConsultaModel
{
    [Column("id")]
    public int Id { get; set; }

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    public ICollection<ConsultaModel> Consultas { get; set; } = null!;
}
