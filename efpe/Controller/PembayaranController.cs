using System;
using System.Data;
using System.Drawing;
using efpe.Model.Entity;
using efpe.Model.Repository;
using MySql.Data.MySqlClient;

namespace efpe.Controller
{
    internal class PembayaranController
    {
        private readonly DbContext _con = new DbContext();

        public bool AddData(PembayaranEntity newPembayaran)
        {
            try
            {
                PembayaranRepository repository = new PembayaranRepository();
                return repository.AddData(newPembayaran);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in PembayaranController.AddData: {ex.Message}");
                return false;
            }
        }

        public DataTable GetData(string query)
        {
            try
            {
                if (_con == null)
                {
                    return null;
                }

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, _con.GetConnection()))
                {
                    DataTable result = new DataTable();
                    adapter.Fill(result);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetData: {ex.Message}");
                return null;
            }
        }

        public DateTime GetWaktuSelesai(int nomorKomputer)
        {
            try
            {
                PembayaranRepository repo = new PembayaranRepository();
                DateTime? waktuSelesai = repo.GetWaktuSelesai(nomorKomputer);
                return waktuSelesai.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetWaktuSelesai: {ex.Message}");
                throw;
            }
        }

    }
}
