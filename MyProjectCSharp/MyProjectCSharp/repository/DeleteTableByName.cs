using System;
using Npgsql;


public class DeleteTableByName
{
    private readonly string _connectionString;

    public void Execute(string tableName, string columnsDefinition, string filtVal, string updCol, string newV) { }

    public void Execute(string tableName, string columnsDefinition)
    {

    }

    public void Execute(string tableName)
    {

    }
    public DeleteTableByName(string connectionString)
    {
        _connectionString = connectionString;
    }


    public void Execute()
    {
        try
        {
            Console.Write("Введите имя таблицы для удаления: ");
            string tableName = Console.ReadLine();
       

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
               

                string sql = $"DROP TABLE IF EXISTS \"{tableName}\"";
              

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Таблица '{tableName}' была успешно удалена (или не существовала).");
                }
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Ошибка базы данных при удалении таблицы: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка при удалении таблицы: {ex.Message}");
        }
      

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}