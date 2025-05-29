using System;
using Npgsql;


public class FindColumnsInTable
{
    private readonly string _connectionString;

    public void Execute(string tableName)
    {

    }
    public FindColumnsInTable(string connectionString)
    {
        _connectionString = connectionString;
    }


    public void Execute()
    {
        try
        {
            Console.Write("Введите имя таблицы для отображения столбцов: ");
            string tableName = Console.ReadLine();
            

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                

                string sql = "SELECT column_name FROM information_schema.columns WHERE table_name = @tableName AND table_schema = 'public' ORDER BY ordinal_position;";
               

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("tableName", tableName);
                    

                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine($"Столбцы таблицы '{tableName}':");
                        bool hasColumns = false;
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0));
                            hasColumns = true;
                        }
                        if (!hasColumns)
                        {
                            Console.WriteLine("Нет столбцов или таблица не найдена.");
                        }
                    }
                }
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Ошибка базы данных при получении столбцов: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка при получении столбцов: {ex.Message}");
        }
       
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}