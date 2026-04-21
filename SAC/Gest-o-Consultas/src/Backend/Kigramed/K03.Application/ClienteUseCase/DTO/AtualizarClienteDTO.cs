using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.ClienteUseCase.DTO;

public class AtualizarClienteDTO
{
     [Required(ErrorMessage = "Nome é obrigatório")]
     public string ClienteNome{get;set;}=string.Empty;

    public string ClienteNif{get;set;}=string.Empty;
}
