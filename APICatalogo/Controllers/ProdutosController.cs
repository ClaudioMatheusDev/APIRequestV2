using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnityOfWork _uof;//propriedade do tipo IUnityOfWork

    public ProdutosController(IUnityOfWork uof)//construtor que recebe um objeto do tipo IUnityOfWork
    {
        _uof = uof;
    }

    [HttpGet("produtos/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id) //método que retorna uma lista de produtos ordenados por Categoria 
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id); // retorna uma lista de produtos ordenados por Categoria
        if (produtos is null)
        {
            return NotFound();
        }
        return Ok(produtos);
    }



    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll(); //retorna uma lista de produtos
        if (produtos is null)
        {
            return NotFound();
        }
        return Ok(produtos);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        var novoProduto = _uof.ProdutoRepository.Create(produto);//cria o produto
        _uof.Commit(); //salva as alterações no banco de dados

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProduto.ProdutoId }, novoProduto);//retorna o objeto criado
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();//erro 400
        }

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);//atualiza o produto
        _uof.Commit();//salva as alterações no banco de dados

        return Ok(produtoAtualizado);

    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id); //retorna o produto com o id passado como parâmetro

        if (produto is null)
        {
            return NotFound();
        }

        var produtoDeletado = _uof.ProdutoRepository.Delete(produto); //deleta o produto
        _uof.Commit();//salva as alterações no banco de dados

        return Ok(produtoDeletado);

    }
}