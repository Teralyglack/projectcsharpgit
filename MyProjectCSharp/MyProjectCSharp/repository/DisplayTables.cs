using System;
using Npgsql;


public class DisplayTables
{
    private readonly string _connectionString;


    public DisplayTables(string connectionString)
    {
        _connectionString = connectionString;
    }


    public void Execute()
    {
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            conn.Open();
           

            string sql = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';";
            

            using (var cmd = new NpgsqlCommand(sql, conn))
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
}
