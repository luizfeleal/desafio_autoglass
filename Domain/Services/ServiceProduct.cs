using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly IProduct _IProduct;

        public ServiceProduct(IProduct IProduct)
        {
            _IProduct = IProduct;
        }

        public async Task Create(Product Objeto)
        {
            var validateDescription = Objeto.ValidateStringProperty(Objeto.Description, "Description");
            var validateManufactureDate = Objeto.ValidateCompareDateProperty(Objeto.ManufactureDate, Objeto.ExpiryDate, "Manufacture Date");

            if(validateDescription && validateManufactureDate)
            {
                await _IProduct.Add(Objeto);
            }
        }

        public async Task<List<Product>> ListWithFilter(Product Objeto, int pageIndex, int pageSize)
        {
            var teste = Objeto;
            if (Objeto == null)
            {
                // Se o objeto de filtro for nulo, retornar todos os produtos
                return await _IProduct.List();
            }
            else
            {
                // Caso contrário, criar uma expressão de filtro com base nas propriedades do objeto
                Expression<Func<Product, bool>> filterExpression = p =>
                    (Objeto.ProductId == 0 || p.ProductId == Objeto.ProductId) &&
                    (Objeto.Description == null || p.Description.Contains(Objeto.Description)) &&
                    (Objeto.Active == false || p.Active == Objeto.Active) &&
                    (Objeto.ManufactureDate == DateTime.MinValue || p.ManufactureDate == Objeto.ManufactureDate) &&
                    (Objeto.ExpiryDate == DateTime.MinValue || p.ExpiryDate == Objeto.ExpiryDate) &&
                    (Objeto.SupplierId == 0 || p.SupplierId == Objeto.SupplierId) &&
                    (Objeto.SupplierDescription == null || p.SupplierDescription.Contains(Objeto.SupplierDescription)) &&
                    (Objeto.SupplierCNPJ == null || p.SupplierCNPJ.Contains(Objeto.SupplierCNPJ));

                var filteredProducts = await _IProduct.ListWithFilter(filterExpression);
                return filteredProducts.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public async Task Update(Product Objeto)
        {
            var validateDescription = Objeto.ValidateStringProperty(Objeto.Description, "Description");
            var validateManufactureDate = Objeto.ValidateCompareDateProperty(Objeto.ManufactureDate, Objeto.ExpiryDate, "Manufacture Date");

            if (validateDescription && validateManufactureDate)
            {
                await _IProduct.Update(Objeto);
            }
        }
        
        public async Task Delete(int productId)
        {
            var product = await _IProduct.GetEntityById(productId);
            if (product != null)
            {
                product.Active = false;
                await _IProduct.Update(product);
            }
        }
    }
}
