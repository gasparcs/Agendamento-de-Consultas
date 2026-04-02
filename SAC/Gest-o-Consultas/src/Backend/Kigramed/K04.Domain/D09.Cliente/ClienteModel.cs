using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D04.Contacto;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.D14.Pagamento;

namespace Kigramed.K04.Domain.D09.Cliente;
[Table("tb09_cliente")]
public class ClienteModel
{
    [Column("nif_cliente")]
    public string Nif_cliente { get; set; } = string.Empty;

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    public ICollection<ContactoModel> Contactos { get; set; } = [];

    public ICollection<PacienteModel> Pacientes { get; set; } = []; 

    public ICollection<PagamentoModel> Pagamentos { get; set; } = [];

}
