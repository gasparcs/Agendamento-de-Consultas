using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.ServicoUseCase.DTO;

public class AtualizarServicoDTO
{
   
   public int ServicoId{get;set;}

    [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
    public String ServicoNome{get;set;}=string.Empty;
   
    [Required(ErrorMessage = "A duração do serviço é obrigatória.")]
    public int ServicoDuracaoMinuto{get;set;}

    [Required(ErrorMessage = "O preço do serviço é obrigatório.")]
    public decimal ServicoPreco{get;set;}

    [Required(ErrorMessage = "O estado do serviço é obrigatório.")]
    public bool ServicoEstado{get;set;} 
}

