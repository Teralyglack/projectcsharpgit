using System;
using Npgsql;


public class DisplayTableColumns
{
    private readonly string _connectionString;


    public DisplayTableColumns(string connectionString)
    {
        _connectionString = connectionString;
    }


    public void Execute(string tableName, NpgsqlConnection externalConnection = null)
    {
        bool ownConnection = false;
        NpgsqlConnection conn = externalConnection;
        

        try
        {
            if (conn == null)
            {
                conn = new NpgsqlConnection(_connectionString);
                conn.Open();
                ownConnection = true;
            }
       

            string sql = "SELECT column_name FROM information_schema.columns WHERE table_name = @tableName;";
       
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("tableName", tableName);
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"Столбцы таблицы '{tableName}':");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0));
                    }
                }
            }
        }
        finally
        {
            if (ownConnection && conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}