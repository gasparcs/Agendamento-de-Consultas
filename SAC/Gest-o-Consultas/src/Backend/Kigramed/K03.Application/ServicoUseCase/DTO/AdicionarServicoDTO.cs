using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.ServicoUseCase.DTO;

public class AdicionarServicoDTO
{
    [Required(ErrorMessage ="Nome é obrigatório")]
   public string ServicoNome{get;set;}=string.Empty;

   [Required(ErrorMessage ="Preço é obrigatório")]  
   public decimal ServicoPreco{get;set;}
}
