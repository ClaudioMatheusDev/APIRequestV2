using APICatalogo.Models;
using System.Runtime.InteropServices;

namespace APICatalogo.Repositories
{
    public interface IProdutoRepository : IRepository<Produto> //interface que herda da interface IRepository 
     {
        IEnumerable<Produto> GetProdutosPorCategoria(int id); //método que retorna uma lista de produtos ordenados por Categoria

    }
}
