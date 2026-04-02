using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D12.Paciente;

namespace Kigramed.K04.Domain.D11.ClientePaciente;
[Table("tb11_cliente_paciente")]
public class ClientePacienteModel
{
    [Column("id")] 
    public int Id { get; set; }

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    public ICollection<PacienteModel> Pacientes { get; set; } = [];

}
