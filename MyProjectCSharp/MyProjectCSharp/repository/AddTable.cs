using System;
using Npgsql;

public class AddTable {

    public void Execute(string tableName, string columnsDefinition, string filtVal, string updCol, string newV) { }

    public void Execute(string tableName, string columnsDefinition)
    {
        
    }

    public void Execute(string tableName)
    {

    }



    private readonly string _connectionString;

    public AddTable(string connectionString)
    {
        _connectionString = connectionString;
    }


    public void Execute()
    {
        Console.Write("Введите имя новой таблицы: ");
        string tableName = Console.ReadLine();
        Console.Write("Введите определение столбцов (например, 'id SERIAL PRIMARY KEY, name VARCHAR(100)'): ");
        string columnsDefinition = Console.ReadLine();
       

        using (var conn = new NpgsqlConnection(_connectionString))
        {
            conn.Open();
            string sql = $"CREATE TABLE IF NOT EXISTS \"{tableName}\" ({columnsDefinition});";
         
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Таблица '{tableName}' успешно добавлена.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении таблицы: {ex.Message}");
                }
            }
        }
    ;

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}