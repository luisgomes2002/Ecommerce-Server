using MySql.Data.MySqlClient;
using Server.Models;

namespace Server.Data.DataBaseTables
{
	public class ProductsDbConnetions(IConfiguration iConfiguration)
	{
		private readonly string? connectionString = iConfiguration.GetConnectionString("DataBase");

		public async Task CreateProduct(ProductsModel productsModel)
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
	}
}
