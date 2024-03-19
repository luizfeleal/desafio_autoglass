using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using ManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private readonly IMapper _IMapper;
        private readonly IProduct _IProduct;
        private readonly IServiceProduct _IServiceProduct;

        public  ProductController(IMapper IMapper, IProduct IProduct, IServiceProduct IServiceProduct) 
        {
            _IMapper = IMapper;
            _IProduct = IProduct;
            _IServiceProduct = IServiceProduct;
        }




        [Produces("application/json")]
        [HttpPost("/api/products")]
        public async Task<List<Notifies>> Add(ProductViewModel product)
        {
            var productMap = _IMapper.Map<Product>(product);
            await _IServiceProduct.Create(productMap);

            return productMap.Notifications;
        }


        [Produces("application/json")]
        [HttpPut("/api/products")]
        public async Task<List<Notifies>> Update(ProductViewModel product)
        {
            var productMap = _IMapper.Map<Product>(product);
            await _IServiceProduct.Update(productMap);

            return productMap.Notifications;
        }

        [Produces("application/json")]
        [HttpDelete("/api/products/{productId}")]
        public async Task<List<Notifies>> Delete(int productId)
        {
            var notifies = new List<Notifies>();

            try
            {
                var product = await _IProduct.GetEntityById(productId);
            
                if (product == null)
                {
                notifies.Add(new Notifies { Message = "Produto não encontrado." });
                    return notifies;
                }
                await _IServiceProduct.Delete(productId);
                notifies.Add(new Notifies { SuccessMessage = "Produto excluído com sucesso." });
            }
            catch (Exception ex)
            {
                notifies.Add(new Notifies { Message = "Erro ao excluir o produto: " + ex.Message });
            }

            return notifies;
        }


        [Produces("application/json")]
        [HttpGet("/api/products/{productId}")]
        public async Task<ProductViewModel> GetEntityById(int productId)
        {
            var product = await _IProduct.GetEntityById(productId);

            var productMap = _IMapper.Map<ProductViewModel>(product);

            return productMap;
        }

        [Produces("application/json")]
        [HttpGet("/api/products/")]
        public async Task<List<ProductViewModel>> List()
        {
            var products = await _IProduct.List();
            var productMap = _IMapper.Map<List<ProductViewModel>>(products);
            return productMap;
        }
        
        [Produces("application/json")]
        [HttpPost("/api/products/filtered/")]
        public async Task<List<ProductViewModel>> ListWithFilter([FromBody] ProductViewModel filter, int pageIndex, int pageSize)
        {
            var modelFilter = _IMapper.Map<Product>(filter);

            var products = await _IServiceProduct.ListWithFilter(modelFilter, pageIndex, pageSize);
            var productMap = _IMapper.Map<List<ProductViewModel>>(products);
            return productMap;
        }


    }
}
