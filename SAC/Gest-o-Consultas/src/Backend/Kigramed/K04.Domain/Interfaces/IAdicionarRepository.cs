using System;

namespace Kigramed.K04.Domain.Interfaces;

public interface IAdicionarRepository <T>
{
 // Podemos adiconar usuários, consultas, pacientes, etc.
    Task<string> AddAsync(T model);

} 
