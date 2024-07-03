using Server.Models;

namespace Server.Repositories.Interfaces
{
	public interface IProductsRepository
    {
		Task<List<ProductsModel>> FindAllProducts();
		Task<ProductsModel> FindProductById(int id);
		Task<ProductsModel> CreateProduct(ProductsModel product, int userId);
		Task<ProductsModel> UpdateProduct(ProductsModel product, int id);
		Task<bool> DeleteProduct(int id);
	}
}