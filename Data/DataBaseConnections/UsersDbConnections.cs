using MySql.Data.MySqlClient;
using Server.Models;

namespace Server.Data.DataBaseTables
{
    public class UsersDbConnections(IConfiguration iConfiguration)
    {
        private readonly string? connectionString = iConfiguration.GetConnectionString("DataBase");

        public async Task CreateUserDb(UsersModel usersModel)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                INSERT INTO Users (Name, Password, Email, CreatedAt)
                VALUES (@Name, @Password, @Email, NOW());";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", usersModel.Name);
                    command.Parameters.AddWithValue("@Password", usersModel.Password);
                    command.Parameters.AddWithValue("@Email", usersModel.Email);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0) Console.WriteLine("User created successfully.");
                    else Console.WriteLine("Failed to create user.");
                }
            }
        }

        public async Task<UsersModel?> FindUserByIdDb(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT UserId, Name, Password, Email, CreatedAt FROM Users WHERE UserId = @UserId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new UsersModel
                            {
                                Id = reader.GetInt32("UserId"),
                                Name = reader.GetString("Name"),
                                Password = reader.GetString("Password"),
                                Email = reader.GetString("Email"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
                            };
                        }
                    }

                }

                return null;
            }
        }

        public async Task<List<UsersModel>> FindAllUsersDb()
        {
            var users = new List<UsersModel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT UserId, Name, Password, Email, CreatedAt FROM Users";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            users.Add(new UsersModel
                            {
                                Id = reader.GetInt32("UserId"),
                                Name = reader.GetString("Name"),
                                Password = reader.GetString("Password"),
                                Email = reader.GetString("Email"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
                            });
                        }
                    }

                }

                return users;
            }
        }

        public async Task<UsersModel?> FindUserByEmailDb(string userEmail)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT Email FROM Users WHERE Email = @Email";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", userEmail);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new UsersModel
                            {
                                Email = reader.GetString("Email"),
                            };
                        }
                    }

                }

                return null;
            }
        }

        public async Task<UsersModel> UpdateUserDb(int UserId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT Email FROM Users WHERE Email = @Email";

            }
        }
    }
}
