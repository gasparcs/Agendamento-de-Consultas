using System;

namespace Kigramed.K04.Domain.Interfaces;

public interface IPegarpeloNifReporitory<T>
{
        Task<T?> PegarpeloNifAsync(string nif);
}
