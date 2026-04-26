using System;
using Kigramed.K04.Domain.D05.Auth;

namespace Kigramed.K04.Domain.Interfaces;

public interface IPegarAuthPeloNifRepository
{
    Task<AuthModel?> PegarPeloNifAsync(string nif);
}