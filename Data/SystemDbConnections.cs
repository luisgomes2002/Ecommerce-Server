using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Server.Models;
using Server.Repositories.Interfaces;
using System.Configuration;

namespace Server.Data
{
    public class SystemDbConnections(IConfiguration iConfiguration)
    {
        private readonly string? connectionString = iConfiguration.GetConnectionString("DataBase");

        public void CreateDataBase()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "CREATE DATABASE IF NOT EXISTS db_ecommerce;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database created successfully or already exists.");
                }
            }
        }
    }
}
