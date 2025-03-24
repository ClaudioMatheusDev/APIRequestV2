using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(IUnityOfWork uof, ILogger<CategoriasController> logger)
    {

        _logger = logger;
        _uof = uof;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categorias = _uof.CategoriaRepository.GetAll();//retorna uma lista de objetos do tipo T (Categoria)
        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id); // c => c.CategoriaId == id é uma expressão lambda que retorna um booleano 

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id= {id} não encontrada...");
            return NotFound($"Categoria com id= {id} não encontrada...");
        }
        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoriaCriada = _uof.CategoriaRepository.Create(categoria);//cria a categoria
        _uof.Commit();//salva as alterações no banco de dados

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        _uof.CategoriaRepository.Update(categoria); //atualiza a categoria
        _uof.Commit();//salva as alterações no banco de dados
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c=> c.CategoriaId == id); 

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id={id} não encontrada...");
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria); //exclui a categoria
        _uof.Commit();//salva as alterações no banco de dados
        return Ok(categoriaExcluida);
     
    }
}