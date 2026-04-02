using System;

namespace Kigramed.K04.Domain.Interfaces;

public interface IActualizarRepository <T>

{
    Task<string> ActualizarAsync(T model);
}
