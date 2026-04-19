using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.ClienteUseCase.DTO;

public class AdicionarClienteDTO
{
     [Required(ErrorMessage = "Nome é obrigatório")]
     public string ClienteNome{get;set;}=string.Empty;

    [Required(ErrorMessage = "Nif é obrigatório")]
    public string ClienteNif{get;set;}=string.Empty;

    [Required(ErrorMessage = "Contactos são obrigatórios")]
    public IEnumerable<AdicionarContactoDTO> Contactos { get; set; } = [];
}

public class AdicionarContactoDTO
{
    public int TipoContacto { get; set; }

    public string Contacto { get; set; } = string.Empty;
}