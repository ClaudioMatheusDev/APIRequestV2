using System.Linq.Expressions;

namespace APICatalogo.Repositories
{
    public interface IRepository<T>
    {

        /// <summary>
        /// Utilizando o principio ISP (Interface Segregation Principle) da SOLID
        /// </summary>

        IEnumerable<T> GetAll();//retorna uma lista de objetos do tipo T
        T? Get(Expression<Func<T, bool>> predicate); // predicate é uma função que recebe um objeto do tipo T e retorna um booleano
        T Create(T entity);//retorna o objeto criado
        T Update(T entity);//retorna o objeto atualizado
        T Delete(T entity);//retorna o objeto deletado

    }
}
