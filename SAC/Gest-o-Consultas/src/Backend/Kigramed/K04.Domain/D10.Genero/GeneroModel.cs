using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D12.Paciente;

namespace Kigramed.K04.Domain.D10.Genero;
[Table("tb10_genero")]
public class GeneroModel
{
    [Column("id")]
    public int Id{get;set;}

    [Column("nome")]
    public string Nome{get;set;}=string.Empty;

    public ICollection<PacienteModel> Pacientes { get; set; } = [];

}
