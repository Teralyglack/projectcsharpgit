using Npgsql;
using System;

class Program
{
    static void Main()
    {
        // Строка подключения к вашей базе данных PostgreSQL
        string mydatabase = "Host=localhost;Database=SiensJournal;Username=postgres;Password=123";

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Главное меню ===");
            Console.WriteLine("1. Просмотреть все таблицы");
            Console.WriteLine("2. Найти определённую таблицу");
            Console.WriteLine("3. Добавить таблицу");
            Console.WriteLine("4. Выход");
            Console.Write("Введите номер команды: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    DisplayTables(mydatabase);
                    break;
                case "2":
                    FindTable(mydatabase);
                    break;
                case "3":
                    AddTable(mydatabase);
                    break;
                case "4":
                    Console.WriteLine("Выход из программы...");
                    return;
                default:
                    Console.WriteLine("Некорректный ввод. Нажмите любую клавишу для повторения.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void DisplayTables(string connectionString)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';", conn))
            using (var reader = cmd.ExecuteReader())
            {
                Console.WriteLine("Список таблиц:");
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                }
            }
        }
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void FindTable(string connectionString)
    {
        Console.Write("Введите имя таблицы для поиска: ");
        string tableName = Console.ReadLine();
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_name = @tableName AND table_schema = 'public';", conn))
            {
                cmd.Parameters.AddWithValue("tableName", tableName);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    Console.WriteLine($"Таблица '{tableName}' найдена.");
                    DisplayTableColumns(conn, tableName);
                }
                else
                {
                    Console.WriteLine($"Таблица '{tableName}' не найдена.");
                }
            }
        }

        static void DisplayTableColumns(NpgsqlConnection conn, string tableName)
        {
            using (var cmd = new NpgsqlCommand("SELECT column_name FROM information_schema.columns WHERE table_name = @tableName;", conn))
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

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void AddTable(string connectionString)
    {
        Console.Write("Введите имя новой таблицы: ");
        string tableName = Console.ReadLine();
        Console.Write("Введите определение столбцов (например, 'id SERIAL PRIMARY KEY, name VARCHAR(100)'): ");
        string columnsDefinition = Console.ReadLine();
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand($"CREATE TABLE IF NOT EXISTS \"{tableName}\" ({columnsDefinition});", conn))
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
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}

