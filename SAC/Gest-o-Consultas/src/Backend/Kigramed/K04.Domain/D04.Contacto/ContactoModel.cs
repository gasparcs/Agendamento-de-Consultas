using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D03.TipoContacto;
using Kigramed.K04.Domain.D09.Cliente;

namespace Kigramed.K04.Domain.D04.Contacto;
[Table("tb04_contacto")]
public class ContactoModel
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nif_funcionario")]
    public string Nif_funcionario { get; set; } = string.Empty;

    [Column("id_tipo_contacto")]
    public string Id_tipo_contacto { get; set; } = string.Empty;

    [Column("nif_cliente")]
    public string Nif_cliente { get; set; } = string.Empty;

    [Column("contacto")]
    public string Contacto { get; set; } = string.Empty;

    public FuncionarioModel Funcionario { get; set; } = null!;

    public TipoContactoModel TipoContacto { get; set; } = null!;

    public ClienteModel Cliente { get; set; } = null!;

}
