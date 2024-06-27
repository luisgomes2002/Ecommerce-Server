using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
		private readonly SystemDbContext _dbContext;

		public ProductsRepository(SystemDbContext systemDbContext)
		{
			_dbContext = systemDbContext;
		}

		public async Task<ProductsModel> FindProductById(int id)
		{
			return await _dbContext.Products
				.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<ProductsModel>> FindAllProducts()
		{
			return await _dbContext.Products
                .Include(x => x.User)
                .ToListAsync();
		}

		public async Task<ProductsModel> CreateProduct(ProductsModel product)
		{
			await _dbContext.Products.AddAsync(product);
			await _dbContext.SaveChangesAsync();

			return product;
		}

		public async Task<ProductsModel> UpdateProduct(ProductsModel product, int id)
		{
            ProductsModel productById = await FindProductById(id);

			if (productById == null) throw new Exception($"Product by id:{id} not found");

            productById.Name = product.Name;
            productById.Value = product.Value;
            productById.Description = product.Description;
			productById.Status = product.Status;
			productById.UserID = product.UserID;

            _dbContext.Products.Update(productById);
			await _dbContext.SaveChangesAsync();

			return productById;
		}

		public async Task<bool> DeleteProduct(int id)
		{
            ProductsModel productById = await FindProductById(id);

			if (productById == null) throw new Exception($"Product by id:{id} not found");

			_dbContext.Products.Remove(productById);
			await _dbContext.SaveChangesAsync();

			return true;
		}
    }
}