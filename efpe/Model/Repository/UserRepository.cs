using System;
using efpe.Model.Entity;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace efpe.Model.Repository
{
    internal class UserRepository
    {
        private readonly DbContext _dbContext = new DbContext();

        public bool RegisterUser(UserEntity newUser)
        {
            try
            {
                string insertQuery = "INSERT INTO users (Username, Email, Password) VALUES (@Username, @Email, @Password)";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", newUser.Username);
                        command.Parameters.AddWithValue("@Email", newUser.Email);
                        command.Parameters.AddWithValue("@Password", newUser.Password);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in RegisterUser: {ex.Message}");
                return false;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public bool AuthenticateUser(string usernameOrEmail, string password)
        {
            try
            {
                string selectQuery = "SELECT * FROM users WHERE Username = @UsernameOrEmail OR Email = @UsernameOrEmail AND Password = @Password";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);
                        command.Parameters.AddWithValue("@Password", password);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            return reader.HasRows;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AuthenticateUser: {ex.Message}");
                return false;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public Tuple<string, string> GetUsernameAndEmail(string usernameOrEmail)
        {
            try
            {
                string selectQuery = "SELECT Username, Email FROM users WHERE Username = @UsernameOrEmail OR Email = @UsernameOrEmail";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string username = reader["Username"].ToString();
                                string email = reader["Email"].ToString();

                                return new Tuple<string, string>(username, email);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetUsernameAndEmail: {ex.Message}");
                return null;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }
    }
}
