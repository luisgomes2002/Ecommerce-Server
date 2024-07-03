using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories;
using Server.Repositories.Interfaces;
using System.Security.Claims;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ProductsController(IProductsRepository productRepository, TokenRepository tokenRepository) : Controller
    {
		private readonly IProductsRepository productRepository = productRepository;
        private readonly TokenRepository tokenRepository = tokenRepository;

		[HttpGet]
		public async Task<ActionResult<List<ProductsModel>>> FindAllProducts()
		{
           List<ProductsModel> products = await productRepository.FindAllProducts();
           return Ok(products);
		}

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductsModel>>> FindProductById(int id)
        {
            ProductsModel product = await productRepository.FindProductById(id);
            return Ok(product);
        }

		[HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductsModel>> CreateProduct([FromBody] ProductsModel productModel)
		{
            try
            {
                var authHeader = Request.Headers.Authorization.ToString();
                var token = authHeader.Replace("Bearer ", "");

                int userId = await tokenRepository.VerifyToken(token);

                ProductsModel product = await productRepository.CreateProduct(productModel, userId);

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar um produto", ex);
            }
		}

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductsModel>> UpdateProduct([FromBody] ProductsModel productModel, int id)
        {
            try
            {
                productModel.Id = id;
                ProductsModel product = await productRepository.UpdateProduct(productModel, id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o produto", ex);
            }
            
        }

		[HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductsModel>> DeleteProduct(int id)
		{
			bool excluded = await productRepository.DeleteProduct(id);
			return Ok(excluded);
		}
    } 
}