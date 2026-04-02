using System;

namespace Kigramed.K04.Domain.Interfaces;
// Esta interface é para encontrar registros específicos com base em um texto.
public interface IPegarpeloTextoRepository <T>
{
    Task<IEnumerable<T>> PegarAsync(string texto); 
}
