using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D07.MedicoEspecialidade;
using Kigramed.K04.Domain.D15.Consulta;

namespace Kigramed.K04.Domain.D19.MedicoConsulta;
[Table("tb19_medico_consulta")]
public class MedicoConsultaModel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_medico_especialidade")]
    public int Id_medico_especialidade { get; set; }

    [Column("id_consulta")]
    public int Id_consulta { get; set; }

    public MedicoEspecilidadeModel MedicoEspecialidade { get; set; } = null!;

    public ConsultaModel Consulta { get; set; } = null!;
}
