using System;

namespace Kigramed.K03.Application.PagamentoUseCase.DTO;

public class LeituraPagamento
{
    public int Id { get; set; }

    public string Cliente { get; set; } = string.Empty;

    public string Secretaria { get; set; } = string.Empty;

    public string Comprovativo { get; set; } = string.Empty;

    public DateTime DataEnvio { get; set; }
}
