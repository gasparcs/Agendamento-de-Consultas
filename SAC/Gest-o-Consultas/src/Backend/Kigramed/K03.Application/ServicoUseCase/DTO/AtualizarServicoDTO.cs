using System;
using System.ComponentModel.DataAnnotations;

namespace Kigramed.K03.Application.ServicoUseCase.DTO;

public class AtualizarServicoDTO
{
    public int ServicoId{get;set;}

    public String ServicoNome{get;set;}=string.Empty;
      
    public int ServicoDuracaoMinuto{get;set;}
   
    public decimal ServicoPreco{get;set;}

    public bool ServicoEstado{get;set;} 
}

