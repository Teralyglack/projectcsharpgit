
using System;
using Services;

class Program
{
    static void Main()
    {
        string connectionString = "Host=localhost;Database=SiensJournal;Username=postgres;Password=123";
     

        var tableService = new TableService(connectionString);
       
        

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Главное меню ===");
            Console.WriteLine("1. Просмотреть все таблицы");
            Console.WriteLine("2. Найти определённую таблицу и вывести столбцы");
            Console.WriteLine("3. Добавить таблицу");
            Console.WriteLine("4. Обновить записи в таблице");
            Console.WriteLine("5. Удалить таблицу");
            Console.WriteLine("6. Поиск столбцов в таблице");
            Console.WriteLine("7. Выход");
            Console.Write("Введите номер команды: ");
           

            string input = Console.ReadLine();
        

            switch (input)
            {
                case "1":
                    tableService.ShowAllTables();
                    break;
                case "2":
                    tableService.FindTableAndShowColumns();
                    break;
                case "3":
                    tableService.AddNewTable();
                    break;
                case "4":
                    tableService.UpdateRecord();
                    break;
                case "5":
                    tableService.DeleteTable();
                    break;
                case "6":
                    tableService.FindColumns();
                    break;
                case "7":
                    Console.WriteLine("Выход из программы...");
                    return;
                default:
                    Console.WriteLine("Некорректный ввод.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}