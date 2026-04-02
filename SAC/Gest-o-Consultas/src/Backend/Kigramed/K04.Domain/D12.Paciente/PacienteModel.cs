using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.D10.Genero;
using Kigramed.K04.Domain.D11.ClientePaciente;
using Kigramed.K04.Domain.D14.Consulta;

namespace Kigramed.K04.Domain.D12.Paciente;
[Table("tb12_paciente")]
public class PacienteModel
{
    [Column("id")]
    public int Id{get;set;}

    [Column("id_cliente")]
    public int Id_cliente {get;set;}

    [Column("nome")]
    public String Nome {get;set;}=string.Empty;

    [Column("data_nascimento")]
    public DateTime Data_nascimento {get;set;}

    [Column("id_genero")]
    public int Id_genero {get;set;} 

    [Column("id_cliente_paciente")]
    public int Id_cliente_paciente {get;set;}

    public ClienteModel Cliente { get; set; } = null!;

    public GeneroModel Genero { get; set; } = null!;

    public ClientePacienteModel ClientePaciente { get; set; } = null!;

    public ICollection<ConsultaModel> Consultas { get; set; } = null!;

}
