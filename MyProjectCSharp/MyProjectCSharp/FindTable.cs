using System;
using Npgsql;

public class FindTable
{
    private readonly string _connectionString;


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