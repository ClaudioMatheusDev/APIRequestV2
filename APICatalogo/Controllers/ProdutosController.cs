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
    private readonly IProdutoRepository _produtoRepository;//injeção de dependência com a interface IProdutoRepository
    private readonly IRepository<Produto> _repository; //injeção de dependência com a interface IRepository
    public ProdutosController(IRepository<Produto> repository, IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
        _repository = repository;
    }

    [HttpGet("produtos/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id) //método que retorna uma lista de produtos ordenados por Categoria 
    {
        var produtos = _produtoRepository.GetProdutosPorCategoria(id); // retorna uma lista de produtos ordenados por Categoria
        if (produtos is null)
        {
            return NotFound();
        }
        return Ok(produtos);
    }



    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _repository.GetAll();
        if (produtos is null)
        {
            return NotFound();
        }
        return Ok(produtos);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.Get(c => c.ProdutoId == id);
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

        var novoProduto = _repository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();//erro 400
        }

        var produtoAtualizado = _repository.Update(produto);

        return Ok(produtoAtualizado);

    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _repository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound();
        }

        var produtoDeletado = _repository.Delete(produto);

        return Ok(produtoDeletado);

    }
}