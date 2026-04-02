using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kigramed.K04.Domain.D07.MedicoEspecialidade;
using Kigramed.K04.Domain.D08.Servico;

namespace Kigramed.K04.Domain.D06.Especialidade;
[Table("tb06_especialidade")]
public class EspecialidadeModel
{
    [Column("id")]
     public int Id{get;set;}

     [Column("nome")]
     public string Nome{get;set;}=string.Empty;

    [Column("descricao")]
     public string Descricao{get;set;}=string.Empty;

     [Column("estado")]
     public bool Estado{get;set;}

     public ICollection<MedicoEspecilidadeModel> MedicoEspecialidades { get; set; } = [];

     public ICollection<ServicoModel> Servicos { get; set; } = [];
}
