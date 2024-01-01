using System;
using System.Collections.Generic;
using System.Data;
using efpe.Controller;
using efpe.Model.Entity;
using MySql.Data.MySqlClient;

namespace efpe.Repository
{
    internal class PromoRepository
    {
        private readonly DbContext _dbContext = new DbContext();
        private readonly PromoController _promoController = new PromoController();

        public List<PromoEntity> GetPromo()
        {
            try
            {
                string query = "SELECT * FROM promo";
                DataTable result = (DataTable)_promoController.GetData(query);

                if (result != null && result.Rows.Count > 0)
                {
                    List<PromoEntity> promos = new List<PromoEntity>();

                    foreach (DataRow row in result.Rows)
                    {
                        PromoEntity promo = new PromoEntity
                        {
                            Nomor = result.Rows.IndexOf(row) + 1,
                            Id = Convert.ToInt32(row["Id"]),
                            Durasi = Convert.ToInt32(row["Durasi"]),
                            Diskon = Convert.ToInt32(row["Diskon"]),
                            KodePromo = row["KodePromo"].ToString(),
                            Expired = Convert.ToDateTime(row["Expired"])
                        };
                        promos.Add(promo);
                    }
                    return promos;
                }
                else
                {
                    Console.WriteLine("No data found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetPromo: {ex.Message}");
                return null;
            }
        }

        public bool AddData(PromoEntity newPromo)
        {
            try
            {
                string insertQuery = "INSERT INTO promo (Durasi, Diskon, KodePromo, Expired) VALUES (@Durasi, @Diskon, @KodePromo, @Expired)";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Durasi", newPromo.Durasi);
                        command.Parameters.AddWithValue("@Diskon", newPromo.Diskon);
                        command.Parameters.AddWithValue("@KodePromo", newPromo.KodePromo);
                        command.Parameters.AddWithValue("@Expired", newPromo.Expired);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddData: {ex.Message}");
                return false;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public bool EditData(PromoEntity updatedPromo)
        {
            try
            {
                string updateQuery = "UPDATE promo SET Durasi = @Durasi, Diskon = @Diskon, KodePromo = @KodePromo, Expired = @Expired WHERE Id = @PromoId";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Durasi", updatedPromo.Durasi);
                        command.Parameters.AddWithValue("@Diskon", updatedPromo.Diskon);
                        command.Parameters.AddWithValue("@KodePromo", updatedPromo.KodePromo);
                        command.Parameters.AddWithValue("@Expired", updatedPromo.Expired);
                        command.Parameters.AddWithValue("@PromoId", updatedPromo.Id);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in EditData: {ex.Message}");
                return false;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public bool DeleteData(int promoId)
        {
            try
            {
                string deleteQuery = "DELETE FROM promo WHERE Id = @PromoId";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PromoId", promoId);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeleteData: {ex.Message}");
                return false;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public bool GetPromoByExpired(DateTime expirationDate)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM promo WHERE Expired = @ExpirationDate";
                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ExpirationDate", expirationDate);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetPromoByExpired: {ex.Message}");
                return false;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public string GetKodePromoByExpired(DateTime expirationDate, int durasi)
        {
            try
            {
                string query = "SELECT KodePromo FROM promo WHERE Expired = @ExpirationDate AND Durasi = @Durasi";
                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                        command.Parameters.AddWithValue("@Durasi", durasi);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        string kodePromo = command.ExecuteScalar() as string;

                        return kodePromo;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetKodePromoByExpired: {ex.Message}");
                return null;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

    }
}
