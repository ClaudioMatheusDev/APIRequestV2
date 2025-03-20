using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repositories //namespace Repositories que contém a classe Repository
{
    public class Repository<T> : IRepository<T> where T : class //T é um tipo genérico que pode ser qualquer classe 
    {

        protected readonly AppDbContext _context; //contexto do banco de dados 

        public Repository(AppDbContext context) //construtor que recebe o contexto do banco de dados
        {
            _context = context; //atribui o contexto do banco de dados ao atributo _context
        }

        public IEnumerable<T> GetAll()//retorna uma lista de objetos do tipo T
        {
            return _context.Set<T>().AsNoTracking().ToList(); // retorna uma lista de objetos do tipo T sem rastreamento7
        }

        public T? Get(Expression<Func<T, bool>> predicate) // predicate é uma função que recebe um objeto do tipo T e retorna um booleano
        {
            return _context.Set<T>().FirstOrDefault(predicate); //retorna o primeiro objeto do tipo T que atende ao predicado
        }
        public T Create(T entity)//retorna o objeto criado
        {
            _context.Set<T>().Add(entity); //adiciona um objeto do tipo T no banco de dados
            //_context.SaveChanges(); //salva as alterações no banco de dados
            return entity; //retorna o objeto criado
        }

        public T Update(T entity)//retorna o objeto atualizado
        {
            _context.Set<T>().Update(entity); //atualiza um objeto do tipo T no banco de dados
            //_context.Entry(entity).State = EntityState.Modified; //atualiza um objeto do tipo T no banco de dados. Definindo o estado da entidade como modificado.
           // _context.SaveChanges(); //salva as alterações no banco de dados 
            return entity; //retorna o objeto atualizado
        }
        public T Delete(T entity) //retorna o objeto deletado
        {
            _context.Set<T>().Remove(entity); //remove um objeto do tipo T no banco de dados
            //_context.SaveChanges(); //salva as alterações no banco de dados
            return entity; //retorna o objeto deletado 
        }
    }

}