using System;

namespace Kigramed.K03.Application.ServicoUseCase.DTO;

public class ListarServicosDTO
{

    public String ServicoNome{get;set;}=string.Empty;
  
    public int ServicoDuracaoMinuto{get;set;}
    
    public decimal ServicoPreco{get;set;}

    public bool ServicoEstado{get;set;}
   
    public int IdEspecialidade{get;set;}
}
