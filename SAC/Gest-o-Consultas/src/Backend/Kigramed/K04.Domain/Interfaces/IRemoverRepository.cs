using System;

namespace Kigramed.K04.Domain.Interfaces;

// Esta interface é para remover os dados.
public interface IRemoverRepository <T>
{
    Task<string> RemoverAsync(T model);
}
