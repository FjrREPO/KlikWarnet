using efpe.Model.Entity;
using efpe.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace efpe.Controller
{
    internal class ItemController
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

        public bool AddItem(ItemEntity newItem)
        {
            try
            {
                ItemRepository itemRepository = new ItemRepository();
                return itemRepository.AddItem(newItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddItem (ItemController): {ex.Message}");
                return false;
            }
        }

        public bool UpdateItem(ItemEntity updatedItem)
        {
            try
            {
                ItemRepository itemRepository = new ItemRepository();
                return itemRepository.UpdateItem(updatedItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateItem (ItemController): {ex.Message}");
                return false;
            }
        }

        public bool DeleteItem(int itemId)
        {
            try
            {
                ItemRepository itemRepository = new ItemRepository();
                return itemRepository.DeleteItem(itemId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeleteItem (ItemController): {ex.Message}");
                return false;
            }
        }

        public List<ItemEntity> GetItems()
        {
            try
            {
                ItemRepository itemRepository = new ItemRepository();
                return itemRepository.GetItems();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetItems (ItemController): {ex.Message}");
                return null;
            }
        }

        public bool UpdateDigunakan(int nomorKomputer, int digunakan)
        {
            try
            {
                ItemRepository itemRepository = new ItemRepository();
                return itemRepository.UpdateDigunakan(nomorKomputer, digunakan);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateDigunakan (ItemController): {ex.Message}");
                return false;
            }
        }
    }
}
