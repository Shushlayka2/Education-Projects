using System;
using System.Diagnostics;
using System.Threading;

namespace Second
{
    class Program
    {
        const string Path = @"..\..\..\..\Exe\";

        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует система управления проектами!");
            Console.WriteLine("Выполнил Латыпов Булат, студент гр. 09-551");
            Console.WriteLine("Формирование пары:");
            Console.WriteLine("Введите два номера проектов для дальнейшего запуска! (от 1 до 5)");
            string answer = Console.ReadLine();
            string[] separate = answer.Split(' ');
            if (separate.Length != 2)
            {
                Console.WriteLine("Неверное количество параметров!");
                SystemClosing();
                return;
            }
            Console.WriteLine("В данной паре выполнить первую программу? (д / н)");
            answer = Console.ReadLine();
            string fileName;
            if (answer.ToLower() == "д")
                fileName = Path + separate[0] + ".exe";
            else
            {
                Console.WriteLine("Автоматический запуск второй программы...");
                fileName = Path + separate[1] + ".exe";
            }
            Run(fileName);
            SystemClosing();
        }

        static void Run(string fileName)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = fileName;
                p.Start();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void SystemClosing()
        {
            Console.WriteLine("Завершение работы системы...");
            Thread.Sleep(1000);
        }
    }
}
