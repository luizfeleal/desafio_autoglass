using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManagerAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectManagerApi
{
    [TestClass]
    public class ProductControllerTests
    {
        private const string BaseUrl = "https://localhost:44345"; // Atualize a URL de acordo com o seu ambiente

        [TestMethod]
        public async Task TestAddProduct()
        {
            // Preparação
            var product = new ProductViewModel
            {
                Description = "Test Product",
                Active = true,
                ManufactureDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddYears(1),
                SupplierId = 1,
                SupplierDescription = "Test Supplier",
                SupplierCNPJ = "12345678901234"
            };

            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
         
            var response = await client.PostAsync($"{BaseUrl}/api/Produtos", content);
 
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            
        }

        [TestMethod]
        public async Task TestUpdateProduct()
        {
            
            var product = new ProductViewModel
            {
                ProductId = 1, 
                Description = "Updated Test Product",
            };

            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            
            var response = await client.PutAsync($"{BaseUrl}/api/Produtos", content);
       
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            
            
        }

        [TestMethod]
        public async Task TestDeleteProduct()
        {
            
            var productId = 1; 

            var client = new HttpClient();
         
            var response = await client.DeleteAsync($"{BaseUrl}/api/Produtos/{productId}");
       
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            
        }

        [TestMethod]
        public async Task TestGetProductById()
        {
            
            var productId = 1; 

            var client = new HttpClient();
        
            var response = await client.GetAsync($"{BaseUrl}/api/Produtos/{productId}");
      
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            
        }

        [TestMethod]
        public async Task TestListProducts()
        {
            var client = new HttpClient();

            var response = await client.GetAsync($"{BaseUrl}/api/Produtos/");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task TestListProductsWithFilter()
        {
            var filter = new ProductViewModel
            {
                Description = "Test", 
                Active = true,
            };

            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{BaseUrl}/api/Produtos/filtered/", content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}