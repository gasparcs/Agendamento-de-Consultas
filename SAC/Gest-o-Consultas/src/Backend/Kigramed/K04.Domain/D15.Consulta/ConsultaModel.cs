using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D07.MedicoEspecialidade;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.D13.EstadoConsulta;
using Kigramed.K04.Domain.D18.PagamentoConsulta;
using Kigramed.K04.Domain.D19.MedicoConsulta;

namespace Kigramed.K04.Domain.D14.Consulta;
[Table("tb14_consulta")]
public class ConsultaModel
{
    [Column("id")]
    public int Id{ get; set; }
    
    [Column("id_medico_especialidade")]
    public int Id_medico_especialiade{ get; set; }

    [Column("id_servico")]
    public int Id_servico{ get; set; }

    [Column("id_paciente")]
    public int Id_paciente{ get; set; }

    [Column("id_estado_consulta")]
    public int Id_estado_consulta{ get; set; }

    [Column("data_consulta")]
    public DateTime Data_consulta{ get; set; }

   public MedicoConsultaModel MedicoConsulta { get; set; }=null!; 

    public ServicoModel Servico{ get; set; }=null!;

    public PacienteModel Paciente{ get; set; }=null!;

    public EstadoConsultaModel EstadoConsulta{ get; set; }=null!;

    public PagamentoConsultaModel PagamentoConsulta{ get; set; }=null!;

}
