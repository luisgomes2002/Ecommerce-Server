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

	public class ProductsController : Controller
    {
		private readonly IProductsRepository iProductRepository;
        private readonly UsersController usersController;

        public ProductsController(IProductsRepository iProductRepository, UsersController usersController)
        {
            this.iProductRepository = iProductRepository;
            this.usersController = usersController;
        }

        [HttpGet]
		public async Task<ActionResult<List<ProductsModel>>> FindAllProducts()
		{
            try
            { 
                List<ProductsModel> products = await iProductRepository.FindAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao mostrar Todos os Produtos", ex);
            }
		}

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductsModel>>> FindProductById(int productId)
        {
            try
            {
                ProductsModel product = await iProductRepository.FindProductById(productId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao achar um produto", ex);
            }
        }

		[HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductsModel>> CreateProduct([FromBody] ProductsModel productModel)
		{
            try
            {
                int userId = await usersController.GetUserIdByToken();
                ProductsModel product = await iProductRepository.CreateProduct(productModel, userId);

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar um produto", ex);
            }
		}

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductsModel>> UpdateProduct([FromBody] ProductsModel productModel, int productId)
        {
            try
            {
                int userId = await usersController.GetUserIdByToken();

                productModel.Id = productId;
                ProductsModel product = await iProductRepository.UpdateProduct(productModel, productId, userId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o produto", ex);
            }
            
        }

		[HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductsModel>> DeleteProduct(int productId)
		{
            try
            {
                int userId = await usersController.GetUserIdByToken();

                bool excluded = await iProductRepository.DeleteProduct(productId, userId);
                return Ok(excluded);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir um produto", ex);
            }
        
		}
    } 
}