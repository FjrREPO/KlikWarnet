using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using efpe.Controller;
using efpe.Model.Entity;
using MySql.Data.MySqlClient;

namespace efpe.Repository
{
    internal class ItemRepository
    {
        DbContext _dbContext = new DbContext();
        private readonly ItemController _itemController = new ItemController();

        public List<ItemEntity> GetItems()
        {
            try
            {
                string query = "SELECT * FROM items";
                DataTable result = (DataTable)_itemController.GetData(query);

                if (result != null && result.Rows.Count > 0)
                {
                    List<ItemEntity> items = new List<ItemEntity>();

                    foreach (DataRow row in result.Rows)
                    {
                        ItemEntity item = new ItemEntity
                        {
                            Nomor = result.Rows.IndexOf(row) + 1,
                            Id = Convert.ToInt32(row["Id"]),
                            NomorKomputer = Convert.ToInt32(row["NomorKomputer"]),
                            VipAtauReguler = row["VipAtauReguler"].ToString(),
                            Digunakan = Convert.ToBoolean(row["Digunakan"]),
                            Image = ResizeImage(row["Image"] as byte[], maxHeight: 50)
                        };
                        items.Add(item);
                    }
                    return items;
                }
                else
                {
                    Console.WriteLine("No data found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetItems: {ex.Message}");
                return null;
            }
        }

        private byte[] ResizeImage(byte[] originalImage, int maxHeight)
        {
            using (MemoryStream ms = new MemoryStream(originalImage))
            {
                using (Image image = Image.FromStream(ms))
                {
                    double ratio = (double)maxHeight / image.Height;
                    int newWidth = (int)(image.Width * ratio);
                    int newHeight = maxHeight;

                    using (Bitmap resizedImage = new Bitmap(newWidth, newHeight))
                    {
                        using (Graphics graphics = Graphics.FromImage(resizedImage))
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                        }

                        using (MemoryStream resultStream = new MemoryStream())
                        {
                            resizedImage.Save(resultStream, image.RawFormat);
                            return resultStream.ToArray();
                        }
                    }
                }
            }
        }

        public bool AddItem(ItemEntity newItem)
        {
            try
            {
                string insertQuery = "INSERT INTO items (NomorKomputer, VipAtauReguler, Digunakan, Image) VALUES (@NomorKomputer, @VipAtauReguler, @Digunakan, @Image)";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NomorKomputer", newItem.NomorKomputer);
                        command.Parameters.AddWithValue("@VipAtauReguler", newItem.VipAtauReguler);
                        command.Parameters.AddWithValue("@Digunakan", newItem.Digunakan);
                        command.Parameters.AddWithValue("@Image", newItem.Image);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddItem: {ex.Message}");
                return false;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public bool UpdateItem(ItemEntity updatedItem)
        {
            try
            {
                string updateQuery = "UPDATE items SET NomorKomputer = @NomorKomputer, VipAtauReguler = @VipAtauReguler, Digunakan = @Digunakan, Image = @Image WHERE Id = @ItemId";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NomorKomputer", updatedItem.NomorKomputer);
                        command.Parameters.AddWithValue("@VipAtauReguler", updatedItem.VipAtauReguler);
                        command.Parameters.AddWithValue("@Digunakan", updatedItem.Digunakan);
                        command.Parameters.AddWithValue("@Image", updatedItem.Image);
                        command.Parameters.AddWithValue("@ItemId", updatedItem.Id);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateItem: {ex.Message}");
                return false;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public bool DeleteItem(int itemId)
        {
            try
            {
                string deleteQuery = "DELETE FROM items WHERE Id = @ItemId";

                using (MySqlConnection connection = _dbContext.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ItemId", itemId);

                        Console.WriteLine($"Executing query: {command.CommandText}");

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeleteItem: {ex.Message}");
                return false;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }
    }
}