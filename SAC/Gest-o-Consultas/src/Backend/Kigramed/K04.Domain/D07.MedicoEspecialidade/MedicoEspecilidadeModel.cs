using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.D19.MedicoConsulta;

namespace Kigramed.K04.Domain.D07.MedicoEspecialidade;
[Table("tb07_medico_especialidade")]
public class MedicoEspecilidadeModel
{
    [Column("id")]
    public int Id{get;set;}

    [Column("nif_funcionario")]
    public string Nif_funcionario{get;set;}=string.Empty;

    [Column("id_especialidade")]
    public int Id_especialidade{get;set;}

    public FuncionarioModel Funcionario{get;set;}=null!;

    public EspecialidadeModel Especialidade{get;set;}=null!;

    public ICollection<MedicoConsultaModel> MedicoConsultas{get;set;}=null!;

}
