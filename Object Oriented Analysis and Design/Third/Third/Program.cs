using System;
using System.Diagnostics;
using System.Threading;

namespace Third
{
    class Program
    {
        const string Path = @"..\..\..\..\Exe\";

        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует система управления проектами!");
            Console.WriteLine("Выполнил Латыпов Булат, студент гр. 09-551");
            Console.WriteLine("Выберите одну программу из пяти для дальнейшего выполнения!");
            string num = Console.ReadLine();
            string fileName = Path + num + ".exe";
            Console.WriteLine("Сколько раз выполнять данную программу?");
            int amount = int.Parse(Console.ReadLine());
            for (int i = 0; i < amount; i++)
            {
                Run(fileName);
                Console.WriteLine("Выполнена {0} - ая итерация", i + 1);
            }
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
