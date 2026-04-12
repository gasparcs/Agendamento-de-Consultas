using System;

namespace Kigramed.K03.Application.ServicoUseCase.DTO;

public class LeituraServicoDTO
{
  public string ServicoNome{get;set;}=string.Empty;

   public decimal ServicoPreco{get;set;} 

 public EspecialidadeDTO Especialidades {get;set;} =null!;
}


public class EspecialidadeDTO
{
  public string Nome {get;set;}=string.Empty;
}
