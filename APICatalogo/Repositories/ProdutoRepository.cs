using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
       

       public ProdutoRepository(AppDbContext context) : base(context) //construtor que recebe o contexto da Repository
        {
            _context = context;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
           
        }


    }
}
