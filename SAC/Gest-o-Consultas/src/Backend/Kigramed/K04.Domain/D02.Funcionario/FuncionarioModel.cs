using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Kigramed.K04.Domain.D01.Perfil;
using Kigramed.K04.Domain.D04.Contacto;
using Kigramed.K04.Domain.D05.Auth;
using Kigramed.K04.Domain.D07.MedicoEspecialidade;
using Kigramed.K04.Domain.D14.Pagamento;

namespace Kigramed.K04.Domain.D02.Funcionario;
[Table("tb02_funcionario")]
public class FuncionarioModel
{
    [Column("nif")]
    public String Nif { get; set; } = string.Empty;

    [Column("id_perfil")]
    public int Id_Perfil { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("estado")]
    public bool Estado { get; set; }

    public PerfilModel Perfil { get; set; } = null!;

    public ICollection<ContactoModel> Contactos { get; set; } = [];

    public AuthModel Auth { get; set; } = null!;

    public ICollection<MedicoEspecilidadeModel> MedicoEspecialidades { get; set; } = [];

    public ICollection<PagamentoModel> Pagamentos { get; set; } = [];


}
