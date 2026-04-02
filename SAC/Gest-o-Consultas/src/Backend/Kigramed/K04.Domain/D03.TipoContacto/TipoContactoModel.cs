using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Kigramed.K04.Domain.D04.Contacto;

namespace Kigramed.K04.Domain.D03.TipoContacto;
[Table("tb03_tipo_contacto")]

public class TipoContactoModel
{
    [Column("id")]
    public int Id{ get; set; }

    [Column("descricao")]
    public String Descricao { get; set; }= string.Empty;

    public ICollection<ContactoModel> Contactos { get; set; } = [];

}
