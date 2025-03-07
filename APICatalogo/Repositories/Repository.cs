using APICatalogo.Context;
using System.Linq.Expressions;

namespace APICatalogo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T? Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        public T Create(T entity)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
        public T Delete(T entity)
        {
            throw new NotImplementedException();
        }


    }
}
