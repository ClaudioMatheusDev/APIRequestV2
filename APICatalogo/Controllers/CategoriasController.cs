using APICatalogo.DTOS;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

/// <summary>
/// Controlador responsável pelas operações CRUD de Categorias.
/// </summary>
[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly ILogger<CategoriasController> _logger;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="CategoriasController"/>.
    /// </summary>
    /// <param name="uof">Instância de unidade de trabalho para acesso ao repositório.</param>
    /// <param name="logger">Instância de logger para logging.</param>
    public CategoriasController(IUnityOfWork uof, ILogger<CategoriasController> logger)
    {
        _logger = logger;
        _uof = uof;
    }

    /// <summary>
    /// Retorna todas as categorias cadastradas.
    /// </summary>
    /// <returns>Lista de objetos <see cref="CategoriaDTO"/>.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        var categorias = _uof.CategoriaRepository.GetAll();

        if (categorias is null)
            return NotFound("Não existe nenhuma categoria!");

        var categoriasDTO = new List<CategoriaDTO>();
        foreach (var categoria in categorias)
        {
            var categoriaDTO = new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };
            categoriasDTO.Add(categoriaDTO);
        }

        return Ok(categoriasDTO);
    }

    /// <summary>
    /// Retorna uma categoria específica pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador da categoria.</param>
    /// <returns>Objeto <see cref="CategoriaDTO"/> correspondente ao id informado.</returns>
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id= {id} não encontrada...");
            return NotFound($"Categoria com id= {id} não encontrada...");
        }

        var categoriaDTO = new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };

        return Ok(categoriaDTO);
    }

    /// <summary>
    /// Cria uma nova categoria.
    /// </summary>
    /// <param name="categoriaDto">Dados da categoria a ser criada.</param>
    /// <returns>Objeto <see cref="CategoriaDTO"/> criado.</returns>
    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoria = new Categoria()
        {
            CategoriaId = categoriaDto.CategoriaId,
            Nome = categoriaDto.Nome,
            ImagemUrl = categoriaDto.ImagemUrl
        };

        var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
        _uof.Commit();

        var novacategoriaDTO = new CategoriaDTO
        {
            CategoriaId = categoriaCriada.CategoriaId,
            Nome = categoriaCriada.Nome,
            ImagemUrl = categoriaCriada.ImagemUrl
        };

        return new CreatedAtRouteResult("ObterCategoria", new { id = novacategoriaDTO.CategoriaId }, novacategoriaDTO);
    }

    /// <summary>
    /// Atualiza uma categoria existente.
    /// </summary>
    /// <param name="id">Identificador da categoria a ser atualizada.</param>
    /// <param name="categoriaDto">Dados atualizados da categoria.</param>
    /// <returns>Objeto <see cref="CategoriaDTO"/> atualizado.</returns>
    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoria = new Categoria()
        {
            CategoriaId = categoriaDto.CategoriaId,
            Nome = categoriaDto.Nome,
            ImagemUrl = categoriaDto.ImagemUrl
        };

        var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();

        var ctegoriaAtualizadaDTO = new CategoriaDTO
        {
            CategoriaId = categoriaAtualizada.CategoriaId,
            Nome = categoriaAtualizada.Nome,
            ImagemUrl = categoriaAtualizada.ImagemUrl
        };
        return Ok(ctegoriaAtualizadaDTO);
    }

    /// <summary>
    /// Remove uma categoria pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador da categoria a ser removida.</param>
    /// <returns>Objeto <see cref="CategoriaDTO"/> removido.</returns>
    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id={id} não encontrada...");
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();

        var categoriaExcluidaDTO = new CategoriaDTO
        {
            CategoriaId = categoriaExcluida.CategoriaId,
            Nome = categoriaExcluida.Nome,
            ImagemUrl = categoriaExcluida.ImagemUrl
        };
        return Ok(categoriaExcluidaDTO);
    }
}
