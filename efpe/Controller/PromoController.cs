using efpe.Model.Entity;
using efpe.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;

namespace efpe.Controller
{
    internal class PromoController
    {
        private readonly DbContext _con = new DbContext();

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

        public bool AddPromo(PromoEntity newPromo)
        {
            PromoRepository _promoRepository = new PromoRepository();
            return ExecuteRepositoryOperation(() => _promoRepository.AddData(newPromo), "AddPromo");
        }

        public bool EditPromo(PromoEntity updatedPromo)
        {
            PromoRepository _promoRepository = new PromoRepository();
            return ExecuteRepositoryOperation(() => _promoRepository.EditData(updatedPromo), "EditPromo");
        }

        public bool DeletePromo(int promoId)
        {
            PromoRepository _promoRepository = new PromoRepository();
            return ExecuteRepositoryOperation(() => _promoRepository.DeleteData(promoId), "DeletePromo");
        }

        public bool GetPromoByExpired(DateTime expirationDate)
        {
            try
            {
                PromoRepository _promoRepository = new PromoRepository();
                return _promoRepository.GetPromoByExpired(expirationDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetPromoByExpired (PromoController): {ex.Message}");
                throw;
            }
        }

        public string GetKodePromoByExpired(DateTime expirationDate, int durasi)
        {
            try
            {
                PromoRepository _promoRepository = new PromoRepository();
                return _promoRepository.GetKodePromoByExpired(expirationDate, durasi);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetKodePromoByExpired (PromoController): {ex.Message}");
                throw;
            }
        }

        private bool ExecuteRepositoryOperation(Func<bool> repositoryOperation, string operationName)
        {
            try
            {
                return repositoryOperation();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in {operationName} (PromoController): {ex.Message}");
                return false;
            }
        }
    }
}
