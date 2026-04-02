using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.D18.PagamentoConsulta;

namespace Kigramed.K04.Domain.D14.Pagamento;
    [Table("tb14_pagamento")]
public class PagamentoModel
{
    [Column("id")]
    public int Id{get;set;}

    [Column("id_cliente")]
    public int Id_cliente{get;set;}

    [Column("id_secretaria")]
    public string Nif_funcionario{get;set;}=string.Empty;
    
    [Column("comprovativo")]
    public string Comprovativo{get;set;}=string.Empty;

    [Column("data_envio")]
    public DateTime Data_envio{get;set;}

    public ClienteModel Cliente { get; set; } = null!;

    public FuncionarioModel Funcionario { get; set; } = null!;

    public ICollection<PagamentoConsultaModel> PagamentoConsultas { get; set; } = [];
    
    }
