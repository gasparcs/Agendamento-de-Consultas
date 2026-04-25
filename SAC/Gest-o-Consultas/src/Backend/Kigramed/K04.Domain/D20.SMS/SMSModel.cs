using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D09.Cliente;

namespace Kigramed.K04.Domain.D20.SMS;

[Table("tb20_sms")]
public class SMSModel
{
  [Column("id")]
  public int Id { get; set;}

  [Column("nif_funcionario")]
  public string Nif_funcionario = string.Empty;

  [Column("id_cliente")]
  public string Id_cliente = string.Empty;

  [Column("data_envio")]
  public DateTime Data_envio {get; set;}

  [Column("mensagem")]
  public string Mensagem = string.Empty;

  [Column("estado")]
  public bool Estado {get;set;}

  public FuncionarioModel Funcionario {get;set;} = null!;
  public ClienteModel Cliente {get;set;} = null!;
}
