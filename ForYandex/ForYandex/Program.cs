using System;
using System.IO;
using System.Threading;

namespace ForYandex
{
    class Program
    {
        static string filePath;
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует система для работы с базами данных.");
            Console.WriteLine("\n\n\n-------------------- Создание базы данных --------------------\n");
            if ((args.Length == 0) || (!File.Exists(args[0])))
                Console.WriteLine("Не найден файл с данными!");
            else
            {
                filePath = args[0];
                bool isExcepted;
                CreateTables(out isExcepted);
                if (!isExcepted)
                    DialogForQueries();
            }
            Thread.Sleep(1000);
        }

        static void CreateTables(out bool isExcepted)
        {
            try
            {
                if (File.Exists(@"..\..\MainDB.db"))
                    File.Delete(@"..\..\MainDB.db");
                DB.CreateTables();
                DB.FillTheProductTable();
                DB.FillTheOrderTable(filePath);
                Console.WriteLine("База данных успешно создана!");
                isExcepted = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isExcepted = true;
            }
        }

        static void DialogForQueries()
        {
            Console.WriteLine("\n\n\n-------------------- Выполнение запросов --------------------");
            while (true)
            {
                Console.WriteLine(@"
1. Вывести количество и сумму заказов по каждому продукту за текущей месяц.
2. Вывести все продукты, которые были заказаны в текущем месяце, но которых не было в прошлом.
3. Вывести все продукты, которые были только в прошлом месяце, но не в текущем, а какие — только в текущем месяце, но не в прошлом.
3. Помесячно вывести продукт, по которому была максимальная сумма заказов за этот период, cумму по этому продукту и его долю от общего объема за этот период.");
                Console.WriteLine("\nВведите номер запроса для выполнения:");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        DB.ExecuteFirstQuery();
                        break;
                    case "2":
                        DB.ExecuteSecondQuery();
                        break;
                    case "3":
                        DB.ExecuteThirdQuery();
                        break;
                    case "4":
                        DB.ExecuteFourthQuery();
                        break;
                    default:
                        Console.WriteLine("Вы не ввели корректный номер запроса!");
                        break;
                }
                Console.WriteLine("\nХотите выполнить еще один запрос? (y/n)");
                answer = "";
                while ((answer != "y") && (answer != "n"))
                    answer = Console.ReadLine();
                if (answer == "n")
                    break;
            }
            Console.WriteLine("Спасибо за работу!");
        }
    }
}
