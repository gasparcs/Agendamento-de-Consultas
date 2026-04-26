using System;

namespace Kigramed.K03.Application.SmsUseCase.DTO;

public class SMSDTO
{
    public int SMSId { get; set; }
    public string SMSMensagem { get; set; } = string.Empty;
    public bool SMSEstado { get; set; }
    public DateTime SMSData { get; set; }
    public string SMSNif_funcionario { get; set; } = string.Empty;
    public string SMSFuncionario { get; set; } = string.Empty;
    public string SMSId_cliente { get; set; } = string.Empty;
    public string SMSCliente { get; set; } = string.Empty;

}
