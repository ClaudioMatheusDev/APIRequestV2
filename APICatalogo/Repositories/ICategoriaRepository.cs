using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface ICategoriaRepository : IRepository<Categoria> //interface que herda da interface IRepository
{
    IEnumerable<Categoria> GetCategorias();
    Categoria GetCategoria(int id);
    Categoria Create(Categoria categoria);
    Categoria Update(Categoria categoria);
    Categoria Delete(int id);
}
