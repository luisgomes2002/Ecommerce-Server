using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.DataBaseTables;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories
{
	public class ProductsRepository(ProductsDbConnetions productsDbConnetions, IUsersRepository iUsersRepository) : IProductsRepository
	{
		private readonly ProductsDbConnetions productsDbConnetions = productsDbConnetions;
		private readonly IUsersRepository iUsersRepository = iUsersRepository;

		public async Task<ProductsModel> FindProductById(int productId)
		{
			return await productsDbConnetions.FindProductByIdDb(productId)
				?? throw new KeyNotFoundException($"Product with Id: {productId} not found.");
        }

		public async Task<List<ProductsModel>> FindAllProducts()
		{
			return await productsDbConnetions.FindAllProductsDb() 
				?? throw new KeyNotFoundException($"Products not found.");
		}

		public async Task<ProductsModel> CreateProduct(ProductsModel product, int userId)
		{
			UsersModel userInfo = await iUsersRepository.FindUserById(userId);

			if (!userInfo.IsMod) throw new("Este usuário não tem permissão para criar produtos");

			await productsDbConnetions.CreateProductDb(product);

            return product;
		}

		public async Task<ProductsModel> UpdateProduct(ProductsModel product, int productId, int userId)
		{
			UsersModel userInfo = await iUsersRepository.FindUserById(userId);

			if (!userInfo.IsMod) throw new("Este usu�rio n�o tem permiss�o para utualizar esse produto");

			ProductsModel productById = await FindProductById(productId)
				?? throw new Exception($"Product by id:{productId} not found");

			productById.Name = product.Name;
			productById.Price = product.Price;
			productById.Description = product.Description;
			productById.Status = product.Status;

			_dbContext.Products.Update(productById);
			await _dbContext.SaveChangesAsync();

			return productById;
		}

		public async Task<bool> DeleteProduct(int productId, int userId)
		{
			UsersModel userInfo = await iUsersRepository.FindUserById(userId);

			if (!userInfo.IsMod) throw new("Este usu�rio n�o tem permiss�o para deletar esse produto");

			ProductsModel productById = await FindProductById(productId)
				?? throw new Exception($"Product by id:{productId} not found");

			_dbContext.Products.Remove(productById);
			await _dbContext.SaveChangesAsync();

			return true;
		}
	}
}