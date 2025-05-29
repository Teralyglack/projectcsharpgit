using System;
using Npgsql;

public class FindTable
{

    private readonly string _connectionString;

    public bool Exists(string tableName)
    {
        return true;
    }
  public void DisplayColumns(string tableName)
    {
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            conn.Open();
            string sql = @"
            SELECT column_name 
            FROM information_schema.columns 
            WHERE table_name = @tableName 
              AND table_schema = 'public'
            ORDER BY ordinal_position;";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue( tableName);

                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"Столбцы таблицы '{tableName}':");
                    while (reader.Read())
                    {
                        Console.WriteLine($"- {reader.GetString(0)}");
                    }
                }
            }
        }
    }

    

    public FindTable(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Execute()
    {
        Console.Write("Введите имя таблицы для поиска: ");
        string tableName = Console.ReadLine();
      

        using (var conn = new NpgsqlConnection(_connectionString))
        {
            conn.Open();
          
            string sql = "SELECT table_name FROM information_schema.tables WHERE table_name = @tableName AND table_schema = 'public';";
            
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("tableName", tableName);
                var result = cmd.ExecuteScalar();
                

                if (result != null)
                {
                    Console.WriteLine($"Таблица '{tableName}' найдена.");
                    var displayCols = new DisplayTableColumns(_connectionString);
                    displayCols.Execute(tableName, conn);
                }
                else
                {
                    Console.WriteLine($"Таблица '{tableName}' не найдена.");
                }
            }
        }
     

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}