class Program
{
    static void Main()
    {
        string mydatabase = "Host=localhost;Database=SiensJournal;Username=postgres;Password=123";

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Главное меню ===");
            Console.WriteLine("1. Просмотреть все таблицы");
            Console.WriteLine("2. Найти определённую таблицу");
            Console.WriteLine("3. Добавить таблицу");
            Console.WriteLine("4. Обновить записи в таблице");
            Console.WriteLine("5. Удалить таблицу");
            Console.WriteLine("6. Поиска столбца в таблице");
            Console.WriteLine("7. Выход");
            Console.Write("Введите номер команды: ");
          
            string input = Console.ReadLine();
          
            switch (input)
            {
                case "1":
                    // Создаем объект и вызываем метод
                    var display = new DisplayTables(mydatabase);
                    display.Execute();
                    break;
                   

                case "2":
                    var find = new FindTable(mydatabase);
                    find.Execute();
                    break;
                   

                case "3":
                    var add = new AddTable(mydatabase);
                    add.Execute();
                    break;

                case "4":
                    var update = new UpdateTable(mydatabase);
                    update.Execute();
                    break;

                case "5":
                    var delete = new DeleteTableByName(mydatabase);
                    delete.Execute();
                    break;
                case "6":
                    var FindColumns = new FindColumnsInTable(mydatabase);
                    FindColumns.Execute();
                    break;

                case "7":
                    Console.WriteLine("Выход из программы...");
                    return;
                   

                default:
                    Console.WriteLine("Некорректный ввод. Нажмите любую клавишу для повторения.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}

