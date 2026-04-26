using System;

namespace Kigramed.K04.Domain.Interfaces;

// Esta interface é para pegar os dados, ou seja, ler os dados do banco de dados.
public interface IListagemRepository <T>
{
    Task<IEnumerable<T>> Listagem(int pagina = 1, int quantidade = 20); 
}
