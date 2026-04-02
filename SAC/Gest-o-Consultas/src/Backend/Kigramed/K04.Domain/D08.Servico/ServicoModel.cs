using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.D14.Consulta;

namespace Kigramed.K04.Domain.D08.Servico;
[Table("tb08_servico")]
public class ServicoModel
{
    [Column("id")]
    public int Id{get;set;}

    [Column("id_especialidade")]
    public string Id_especialidade{get;set;}=string.Empty;

    [Column("nome")]
    public string Nome{get;set;}=string.Empty;

    [Column("duracao_minuto")]
    public int Duracao_minuto{get;set;}

    [Column("preco")]
    public decimal Preco{get;set;}

    [Column("estado")]
    public bool Estado{get;set;}

    public EspecialidadeModel Especialidade{get;set;}=null!;

    public ICollection<ConsultaModel> Consultas{get;set;}=null!;

}
