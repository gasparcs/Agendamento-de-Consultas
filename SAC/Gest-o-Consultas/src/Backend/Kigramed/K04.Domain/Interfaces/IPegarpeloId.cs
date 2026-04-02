using System;

namespace Kigramed.K04.Domain.Interfaces;

//Focada em encontrar um registro específico, ou seja, pegar um registro pelo ID.
public interface IPegarpeloId <T>
{
    Task<T?> PegarAsync(int id);
} 
 