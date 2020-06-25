using System;
using System.Diagnostics;
using System.Threading;

namespace First
{
    class Program
    {
        const string Path = @"..\..\..\..\Exe\";

        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует система управления проектами!");
            Console.WriteLine("Выполнил Латыпов Булат, студент гр. 09-551");
            for (int i = 1; i <= 5; i++)
            {
                string fileName = Path + i.ToString() + ".exe";
                Console.WriteLine("Хотите запустить следующую программу? (д / н)");
                string answer = Console.ReadLine();
                switch (answer.ToLower())
                {
                    case "д":
                        Run(fileName);
                        Console.WriteLine("{0} - ая программа завершила свою работу", i);
                        break;
                    case "н":
                        SystemClosing();
                        return;
                    default:
                        Console.WriteLine("Вы ввели некорректный ответ!\nСистема автоматически завершит работу!");
                        Thread.Sleep(1000);
                        return;
                }
            }
            SystemClosing();
        }

        static void Run(string fileName)
        {
            Process p = new Process();
            p.StartInfo.FileName = fileName;
            p.Start();
            p.WaitForExit();
        }

        static void SystemClosing()
        {
            Console.WriteLine("Завершение работы системы...");
            Thread.Sleep(1000);
        }
    }
}
