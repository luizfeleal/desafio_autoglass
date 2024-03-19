using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceProduct
    {
        Task Create(Product Objeto);

        Task Update(Product Objeto);

        Task Delete(int productId);

        Task<List<Product>> ListWithFilter(Product Filter, int pageIndex, int pageSize);
    }
}
