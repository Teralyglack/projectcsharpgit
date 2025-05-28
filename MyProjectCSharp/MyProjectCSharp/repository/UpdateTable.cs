using System;
using Npgsql;

public class UpdateTable
{
    private readonly string _connectionString;


    public UpdateTable(string connectionString)
    {
        _connectionString = connectionString;
    }


    public void Execute()
    {
        Console.Write("Введите имя таблицы для обновления записи: ");
        string tableName = Console.ReadLine();


        Console.Write("Введите имя столбца для фильтрации (например, id): ");
        string filterColumn = Console.ReadLine();
  

        Console.Write("Введите значение для фильтра по этому столбцу: ");
        string filterValue = Console.ReadLine();
       

        Console.Write("Введите имя столбца, который хотите обновить: ");
        string updateColumn = Console.ReadLine();
    
        Console.Write("Введите новое значение: ");
        string newValue = Console.ReadLine();
    
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            conn.Open();
            

            // Формируем SQL-запрос с параметрами для безопасности
            string sql = $"UPDATE \"{tableName}\" SET \"{updateColumn}\" = @newValue WHERE \"{filterColumn}\" = @filterValue";
            

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("newValue", newValue);
                cmd.Parameters.AddWithValue("filterValue", filterValue);
               
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Обновление выполнено успешно.");
                    else
                        Console.WriteLine("Запись с указанным фильтром не найдена.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении: {ex.Message}");
                }
            }
        }
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}