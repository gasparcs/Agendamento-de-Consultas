using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.SmsUseCase.DTO;

public class AtualizarSMSDTO
{

    [Required]
    public int SMSId {get;set;}

    [Required(ErrorMessage = "Informe a mensagem")]
    public string SMSMensagem { get; set; } = string.Empty;

    public bool SMSEstado { get; set; } = true;

    [Required(ErrorMessage = "Informe o funcionário")]
    public string SMSNif_funcionario { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o cliente")]
    public string SMSId_cliente { get; set; } = string.Empty;

}
