using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D04.Contacto;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.D14.Pagamento;
using Kigramed.K04.Domain.D20.SMS;

namespace Kigramed.K04.Domain.D09.Cliente;
[Table("tb09_cliente")]
public class ClienteModel
{
    [Key]
    [Column("nif")]
    public string Nif_cliente { get; set; } = string.Empty;

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    public ICollection<ContactoModel> Contactos { get; set; } = [];

    public ICollection<PacienteModel> Pacientes { get; set; } = []; 

    public ICollection<PagamentoModel> Pagamentos { get; set; } = [];

    public ICollection<SMSModel> Mensagens { get; set; } = [];

}
