using System;

namespace Kigramed.K03.Application.ServicoUseCase.DTO;

public class LeituraServicoDTO
{
  public string ServicoNome{get;set;}=string.Empty;

   public decimal ServicoPreco{get;set;}

 public IEnumerable<EspecialidadeDTO> Especialidades {get;set;}=[];
}


public class EspecialidadeDTO
{
  public string Especialidade{get;set;}=string.Empty;
}
