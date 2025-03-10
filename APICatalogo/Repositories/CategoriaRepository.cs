using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository //classe que herda da Repository e implementa a interface ICategoriaRepository
{

    public CategoriaRepository(AppDbContext context) : base(context) //construtor que recebe o contexto da Repository
    {
    }

}



