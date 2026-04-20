using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.EspecialidadeUseCase.DTO;

public class AtualizarEspecialidadeDTO
{
    public int EspecialidadeId { get; set; }

    [Required(ErrorMessage = "O nome da especialidade é obrigatório.")]
    public string EspecialidadeNome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição da especialidade é obrigatória.")]
    public string EspecialidadeDescricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O estado da especialidade é obrigatório.")]
    public bool EspecialidadeEstado { get; set; }
}
