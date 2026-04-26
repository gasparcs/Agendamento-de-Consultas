using System;
using Kigramed.K04.Domain.D02.Funcionario;

namespace Kigramed.K04.Domain.Interfaces;

public interface IPegarFuncionarioPeloTelefoneRepository
{
    Task<FuncionarioModel?> PegarPeloTelefoneAsync(string telefone);
}