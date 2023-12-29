using MySql.Data.MySqlClient;
using System;
using System.Data;

public class DbContext
{
    string connectionString = "Server=localhost;Database=klikwarnet;Uid=root;Pwd=";
    MySqlConnection con;

    public MySqlConnection GetConnection()
    {
        if (con == null)
        {
            con = new MySqlConnection(connectionString);
        }

        return con;
    }

    public void OpenConnection()
    {
        try
        {
            GetConnection(); // Ensure connection is created
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                Console.WriteLine("Connection opened successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in OpenConnection: {ex.Message}");
        }
    }


    public void CloseConnection()
    {
        con.Close();
        Console.WriteLine("Connection closed.");
    }

    public void ExecuteQuery(string query)
    {
        try
        {
            MySqlCommand command = new MySqlCommand(query, con);
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in ExecuteQuery: {ex.Message}");
        }
    }

}
