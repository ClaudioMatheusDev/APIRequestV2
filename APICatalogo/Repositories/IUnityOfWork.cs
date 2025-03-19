namespace APICatalogo.Repositories
{
    public interface IUnityOfWork //interface IUnityOfWork com as propriedades ProdutoRepository e CategoriaRepository e o método Commit
    {
        IProdutoRepository ProdutoRepository { get; }//propriedade do tipo IProdutoRepository
        ICategoriaRepository CategoriaRepository { get; }//propriedade do tipo ICategoriaRepository
        void Commit(); //salva as alterações no banco de dados
    }
}
