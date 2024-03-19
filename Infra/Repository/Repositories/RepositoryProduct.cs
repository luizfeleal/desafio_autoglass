using Domain.Interfaces;
using Entities.Entities;
using Infra.Configuration;
using Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using System.Linq;


public class RepositoryProduct : RepositoryGenerics<Product>, IProduct
{
    private readonly DbContextOptions<ContextBase> _OptionsBuilder;

    public RepositoryProduct() 
    {
        _OptionsBuilder = new DbContextOptions<ContextBase>();
    }

    public async Task<List<Product>> ListWithFilter(Expression<Func<Product, bool>> exProduct)
    {
        using (var context = new ContextBase(_OptionsBuilder))
        {
            return await context.Product.Where(exProduct).AsNoTracking().ToListAsync();
        }
    }
}