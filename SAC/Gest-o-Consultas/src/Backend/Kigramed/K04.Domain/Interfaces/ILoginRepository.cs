using System;

namespace Kigramed.K04.Domain.Interfaces;

public interface ILoginRepository
{
    Task<string> LoginAsync(string nif, string senha);
}
