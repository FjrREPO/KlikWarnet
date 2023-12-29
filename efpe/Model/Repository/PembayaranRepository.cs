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

        public List<PembayaranEntity> GetPembayarans()
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
                string insertQuery = "INSERT INTO pembayaran (NomorKomputer, Username, Email, VipAtauRegular, MetodePembayaran, KodePromo, Harga, Durasi, WaktuMulai, WaktuSelesai) VALUES (@NomorKomputer, @Username, @Email, @VipAtauRegular, @MetodePembayaran, @KodePromo, @Harga, @Durasi, @WaktuMulai, @WaktuSelesai)";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NomorKomputer", newPembayaran.NomorKomputer);
                        command.Parameters.AddWithValue("@Username", newPembayaran.Username); 
                        command.Parameters.AddWithValue("@Email", newPembayaran.Email); 
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
    }
}
