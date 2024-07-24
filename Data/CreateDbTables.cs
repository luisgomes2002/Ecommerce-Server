using MySql.Data.MySqlClient;

namespace Server.Data
{
    public class CreateDbTables(IConfiguration iConfiguration)
    {
        private readonly string? connectionString = iConfiguration.GetConnectionString("DataBase");

        public void CreateUserTable()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                CREATE TABLE IF NOT EXISTS Users (
                    UserId INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(50) NOT NULL,
                    Password VARCHAR(255) NOT NULL,
                    Email VARCHAR(100) NOT NULL,
                    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("User table created successfully or already exists.");
                }
            }
        }

        public void CreateProductTable()
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                CREATE TABLE IF NOT EXISTS Products (
                    ProductId INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(100) NOT NULL,
                    Description VARCHAR(1000) NOT NULL,
                    Price DECIMAL(10, 2) NOT NULL,
                    Status INT NOT NULL,
                    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Product table created successfully or already exists.");
                }
            }


        }
    }
}
