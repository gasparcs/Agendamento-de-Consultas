using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D14.Pagamento;
using Kigramed.K04.Domain.D15.Consulta;

namespace Kigramed.K04.Domain.D18.PagamentoConsulta;
[Table("tb18_pagamento_consulta")]
public class PagamentoConsultaModel
{
    [Key]
    [Column("id")]
    public int Id{get;set;}

    [Column("id_pagamento")]
    public int Id_Pagamento{get;set;}

    [Column("id_consulta")]
    public int Id_Consulta{get;set;}

    public PagamentoModel Pagamento{get;set;}=null!;

    public ConsultaModel Consulta{get;set;}=null!;


}
