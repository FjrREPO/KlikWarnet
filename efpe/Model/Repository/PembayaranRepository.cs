using System;
using System.Collections.Generic;
using System.Data;
using efpe.Controller;
using efpe.Model.Entity;
using MySql.Data.MySqlClient;

namespace efpe.Model.Repository
{
    internal class PembayaranRepository
    {
        private readonly DbContext _dbContext = new DbContext();
        private readonly PembayaranController _pembayaranController = new PembayaranController();

        public List<PembayaranEntity> GetPembayaran()
        {
            try
            {
                string query = "SELECT * FROM pembayaran";
                DataTable result = _pembayaranController.GetData(query);

                if (result != null && result.Rows.Count > 0)
                {
                    List<PembayaranEntity> pembayarans = new List<PembayaranEntity>();

                    foreach (DataRow row in result.Rows)
                    {
                        PembayaranEntity pembayaran = new PembayaranEntity
                        {
                            Nomor = result.Rows.IndexOf(row) + 1,
                            Id_pembayaran = Convert.ToInt32(row["Id_pembayaran"]),
                            WaktuPembayaran = Convert.ToDateTime(row["WaktuPembayaran"]),
                            NomorKomputer = Convert.ToInt32(row["NomorKomputer"]),
                            Username = row["Username"].ToString(),
                            Email = row["Email"].ToString(),
                            VipAtauRegular = row["VipAtauRegular"].ToString(),
                            MetodePembayaran = row["MetodePembayaran"].ToString(),
                            KodePromo = row["KodePromo"].ToString(),
                            Harga = row["Harga"].ToString(),
                            Durasi = Convert.ToInt32(row["Durasi"]),
                            WaktuMulai = Convert.ToDateTime(row["WaktuMulai"]),
                            WaktuSelesai = Convert.ToDateTime(row["WaktuSelesai"])
                        };

                        pembayarans.Add(pembayaran);
                    }
                    return pembayarans;
                }
                else
                {
                    Console.WriteLine("No data found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetPembayarans: {ex.Message}");
                return null;
            }
        }

        public bool AddData(PembayaranEntity newPembayaran)
        {
            try
            {
                string insertQuery = "INSERT INTO pembayaran (WaktuPembayaran, Username, Email, NomorKomputer, VipAtauRegular, MetodePembayaran, KodePromo, Harga, Durasi, WaktuMulai, WaktuSelesai) VALUES (@WaktuPembayaran, @Username, @Email, @NomorKomputer, @VipAtauRegular, @MetodePembayaran, @KodePromo, @Harga, @Durasi, @WaktuMulai, @WaktuSelesai)";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@WaktuPembayaran", newPembayaran.WaktuPembayaran);
                        command.Parameters.AddWithValue("@Username", newPembayaran.Username); 
                        command.Parameters.AddWithValue("@Email", newPembayaran.Email); 
                        command.Parameters.AddWithValue("@NomorKomputer", newPembayaran.NomorKomputer);
                        command.Parameters.AddWithValue("@VipAtauRegular", newPembayaran.VipAtauRegular);
                        command.Parameters.AddWithValue("@MetodePembayaran", newPembayaran.MetodePembayaran);
                        command.Parameters.AddWithValue("@KodePromo", newPembayaran.KodePromo);
                        command.Parameters.AddWithValue("@Harga", newPembayaran.Harga);
                        command.Parameters.AddWithValue("@Durasi", newPembayaran.Durasi);
                        command.Parameters.AddWithValue("@WaktuMulai", newPembayaran.WaktuMulai);
                        command.Parameters.AddWithValue("@WaktuSelesai", newPembayaran.WaktuSelesai);

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

        public bool UpdateData(PembayaranEntity updatedPembayaran)
        {
            try
            {
                string updateQuery = "UPDATE pembayaran SET NomorKomputer = @NomorKomputer, Username = @Username, Email = @Email, VipAtauRegular = @VipAtauRegular, MetodePembayaran = @MetodePembayaran, KodePromo = @KodePromo, Harga = @Harga, Durasi = @Durasi, WaktuMulai = @WaktuMulai, WaktuSelesai = @WaktuSelesai WHERE NomorKomputer = @NomorKomputer";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NomorKomputer", updatedPembayaran.NomorKomputer);
                        command.Parameters.AddWithValue("@Username", updatedPembayaran.Username); 
                        command.Parameters.AddWithValue("@Email", updatedPembayaran.Email); 
                        command.Parameters.AddWithValue("@VipAtauRegular", updatedPembayaran.VipAtauRegular);
                        command.Parameters.AddWithValue("@MetodePembayaran", updatedPembayaran.MetodePembayaran);
                        command.Parameters.AddWithValue("@KodePromo", updatedPembayaran.KodePromo);
                        command.Parameters.AddWithValue("@Harga", updatedPembayaran.Harga);
                        command.Parameters.AddWithValue("@Durasi", updatedPembayaran.Durasi);
                        command.Parameters.AddWithValue("@WaktuMulai", updatedPembayaran.WaktuMulai);
                        command.Parameters.AddWithValue("@WaktuSelesai", updatedPembayaran.WaktuSelesai);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateData: {ex.Message}");
                return false;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public bool DeleteData(int idPembayaran)
        {
            try
            {
                string deleteQuery = "DELETE FROM pembayaran WHERE Id_pembayaran = @Id_pembayaran";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id_pembayaran", idPembayaran);

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


        public DateTime? GetWaktuSelesai(int nomorKomputer)
        {
            try
            {
                string query = "SELECT WaktuSelesai FROM pembayaran WHERE NomorKomputer = @NomorKomputer AND WaktuSelesai >= @WaktuPembayaran ORDER BY WaktuSelesai DESC LIMIT 1";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NomorKomputer", nomorKomputer);
                        command.Parameters.AddWithValue("@WaktuPembayaran", DateTime.Now);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return (DateTime)result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetWaktuSelesai: {ex.Message}");
                throw;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }
    }
}
