using APICatalogo.Context;

namespace APICatalogo.Repositories
{
    public class UnitOfWork : IUnityOfWork //classe UnitOfWork que implementa a interface IUnityOfWork
    {
        private IProdutoRepository _produtoRepo; //propriedade do tipo IProdutoRepository
        private ICategoriaRepository _categoriaRepo;//propriedade do tipo ICategoriaRepository

        public AppDbContext _context; // propriedade do tipo AppDbContext tem que ser publica para ser acessada por outras classes

        public UnitOfWork(AppDbContext context) //construtor que recebe um objeto do tipo AppDbContext
        {
            _context = context;
        }


        public IProdutoRepository ProdutoRepository//propriedade ProdutoRepository do tipo IProdutoRepository
        {
            get//Método get que retorna _produtoRepo
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context); //se _produtoRepo for nulo, cria uma nova instância de ProdutoRepository
            }

        }

        public ICategoriaRepository CategoriaRepository//propriedade CategoriaRepository do tipo ICategoriaRepository
        {
            get//Método get que retorna _categoriaRepo
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context); //se _categoriaRepo for nulo, cria uma nova instância de CategoriaRepository
            }
        }


        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
