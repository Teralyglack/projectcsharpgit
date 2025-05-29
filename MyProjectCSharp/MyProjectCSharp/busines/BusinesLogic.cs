using Npgsql;
using System;

namespace Services
{
    public class TableService
    {
        private readonly DisplayTables _displayTables;
        private readonly FindTable _findTable;
        private readonly AddTable _addTable;
        private readonly UpdateTable _updateTable;
        private readonly DeleteTableByName _deleteTable;
        private readonly FindColumnsInTable _findColumns;


        public TableService(string connectionString)
        {
            _displayTables = new DisplayTables(connectionString);
            _findTable = new FindTable(connectionString);
            _addTable = new AddTable(connectionString);
            _updateTable = new UpdateTable(connectionString);
            _deleteTable = new DeleteTableByName(connectionString);
            _findColumns = new FindColumnsInTable(connectionString);
        }


        public void ShowAllTables()
        {
            _displayTables.Execute();
            Pause();
        }

        public void FindTableAndShowColumns()
        {
            Console.Write("Введите имя таблицы: ");
            string name = Console.ReadLine();

            if (_findTable.Exists(name))
            {
                Console.WriteLine($"Таблица '{name}' найдена.");
                _findTable.DisplayColumns(name);
            }
            else
            {
                Console.WriteLine("Таблица не найдена.");
            }
            Pause();
        }


        public void AddNewTable() {

            _addTable.Execute();
        }  


        public void UpdateRecord()
        {
            Console.Write("Введите имя таблицы: ");
            string tableName = Console.ReadLine();


            if (!_findTable.Exists(tableName))
            {
                Console.WriteLine("Таблица не найдена.");
                Pause();
                return;
            }

            Console.Write("Поле для фильтрации: ");
            string filterCol = Console.ReadLine();
            Console.Write("Значение фильтра: ");
            string filterVal = Console.ReadLine();
            Console.Write("Поле для обновления: ");
            string updateCol = Console.ReadLine();
            Console.Write("Новое значение: ");
            string newVal = Console.ReadLine();


            _updateTable.Execute(tableName, filterCol, filterVal, updateCol, newVal);
            Pause();
        }


        public void DeleteTable()
        {
            _deleteTable.Execute();
            Pause();
        }

        public void FindColumns()
        {
            Console.Write("Введите имя таблицы для просмотра столбцов: ");
            string name = Console.ReadLine();


            if (!_findTable.Exists(name))
            {
                Console.WriteLine("Таблица не найдена.");
                Pause();
                return;
            }


            _findColumns.Execute(name);
            Pause();
        }


        private void Pause()
        {
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}