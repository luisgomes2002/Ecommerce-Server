using MySql.Data.MySqlClient;
using Serve.Enums;
using Server.Models;

namespace Server.Data.DataBaseTables
{
	public class ProductsDbConnetions(IConfiguration iConfiguration)
	{
		private readonly string? connectionString = iConfiguration.GetConnectionString("DataBase");

		public async Task CreateProductDb(ProductsModel productsModel)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				await connection.OpenAsync();

				string query = @"
                INSERT INTO Products (Name, Description, Price, Status, CreatedAt)
                VALUES (@Name, @Description, @Price, @Status, NOW());";

				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Name", productsModel.Name);
					command.Parameters.AddWithValue("@Description", productsModel.Description);
					command.Parameters.AddWithValue("@Price", productsModel.Price);
					command.Parameters.AddWithValue("@Status", productsModel.Status);

					int rowsAffected = await command.ExecuteNonQueryAsync();

					if (rowsAffected > 0) Console.WriteLine("User created successfully.");
					else Console.WriteLine("Failed to create user.");
				}
			}
		}

        public async Task<ProductsModel?> FindProductByIdDb(int productId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT ProductId, Name, Description, Price, Status, CreatedAt FROM Products WHERE ProductId = @UProductId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new ProductsModel
                            {
                                Id = reader.GetInt32("UserId"),
                                Name = reader.GetString("Name"),
                                Description = reader.GetString("Description"),
                                Price = reader.GetFloat("Price"),
                                Status = (StatusProducts)reader.GetInt32("Status"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
                            };
                        }
                    }

                }

                return null;
            }

        }

        public async Task <List<ProductsModel>> FindAllProductsDb()
        {
            var products = new List<ProductsModel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT * FROM Products";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new ProductsModel
                            {
                                Id = reader.GetInt32("UserId"),
                                Name = reader.GetString("Name"),
                                Description = reader.GetString("Description"),
                                Price = reader.GetFloat("Price"),
                                Status = (StatusProducts)reader.GetInt32("Status"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
                            });
                        }
                    }
                }
                return products;
            }
        }
    }
}
