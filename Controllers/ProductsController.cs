using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ProductsController : Controller
    {
		private readonly IProductsRepository productRepository;
		public ProductsController(IProductsRepository productRepository)
		{
			this.productRepository = productRepository;
		}	

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
		public async Task<ActionResult<ProductsModel>> CreateProduct([FromBody] ProductsModel productModel)
		{
            ProductsModel product = await productRepository.CreateProduct(productModel);
			return Ok(product);
		}

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductsModel>> UpdateProduct([FromBody] ProductsModel productModel, int id)
        {
            productModel.Id = id;
            ProductsModel product = await productRepository.UpdateProduct(productModel, id);
            return Ok(product);
        }

		[HttpDelete("{id}")]
		public async Task<ActionResult<ProductsModel>> DeleteProduct(int id)
		{
			bool excluded = await productRepository.DeleteProduct(id);
			return Ok(excluded);
		}
    } 
}