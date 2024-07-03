using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories
{
	public class ProductsRepository(SystemDbContext systemDbContext, IUsersRepository usersRepository) : IProductsRepository
	{
		private readonly SystemDbContext _dbContext = systemDbContext;
		private readonly IUsersRepository usersRepository = usersRepository;

        public async Task<ProductsModel> FindProductById(int id)
        {
            return await _dbContext.Products
               .FirstOrDefaultAsync(x => x.Id == id)
               ?? throw new KeyNotFoundException($"Product with Id: {id} not found.");
        }

        public async Task<List<ProductsModel>> FindAllProducts()
		{
			return await _dbContext.Products
				.ToListAsync();
		}

		public async Task<ProductsModel> CreateProduct(ProductsModel product, int userId)
		{
			UsersModel userInfo = await usersRepository.FindUserById(userId);

			if(!userInfo.IsMod) throw new ("Este usuário não tem permissão para criar produtos");

            product.UserId = userId;
			product.UserName = userInfo.Name;

            await _dbContext.Products.AddAsync(product);
			await _dbContext.SaveChangesAsync();

			return product; 
        }

		public async Task<ProductsModel> UpdateProduct(ProductsModel product, int id)
		{
            ProductsModel productById = await FindProductById(id) 
				?? throw new Exception($"Product by id:{id} not found");

            productById.Name = product.Name;
            productById.Value = product.Value;
            productById.Description = product.Description;
			productById.Status = product.Status;

            _dbContext.Products.Update(productById);
			await _dbContext.SaveChangesAsync();

			return productById;
		}

		public async Task<bool> DeleteProduct(int id)
		{
            ProductsModel productById = await FindProductById(id) 
				?? throw new Exception($"Product by id:{id} not found");

			_dbContext.Products.Remove(productById);
			await _dbContext.SaveChangesAsync();

			return true;
		}
    }
}