using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;


namespace Fourth
{
    class Program
    {
        const string Path = @"..\..\..\..\Exe\";
        static int lastid;

        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует система управления проектами!");
            Console.WriteLine("Выполнил Латыпов Булат, студент гр. 09-551");
            Console.WriteLine("Выберите одну программу из пяти для дальнейшего выполнения!");
            string num = Console.ReadLine();
            string fileName = Path + num + ".exe";
            Console.WriteLine("Сколько раз выполнять данную программу? (максимум: 9)");
            int amount = int.Parse(Console.ReadLine());
            if ((amount > 9) || (amount == 0))
            {
                if (amount > 9)
                    Console.WriteLine("Невозможно открыть введенное количество окон!");
                SystemClosing();
                return;
            }

            Task t = ParallelRunningAsync(fileName, amount);
            t.Wait();

            WaitTheLastProcessClosing(num);
            SystemClosing();

        }

        static async Task ParallelRunningAsync(string fileName, int amount)
        {
            int x = 0, y = 0;
            Task[] tasks = new Task[amount];
            for (int i = 0; i < amount; i++)
            {
                tasks[i] = RunAsync(fileName, x, y);
                x += 450;
                if (x == 1350)
                {
                    y += 245;
                    x = 0;
                }
            }
            await Task.WhenAll(tasks);
        }

        static Task RunAsync(string fileName, int x, int y)
        {
            return Task.Run(() =>
            {
                Process p = new Process();
                p.StartInfo.FileName = fileName;
                p.Start();
                lastid = p.Id;
                Thread.Sleep(150);
                Program.MoveWindow(p.MainWindowHandle, x, y, 450, 245, true);
            });
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        static void WaitTheLastProcessClosing(string num)
        {
            Process lastP = Process.GetProcessById(lastid);
            lastP.WaitForExit();

            foreach (Process p in Process.GetProcessesByName(num))
                p.Kill();
        }

        static void SystemClosing()
        {
            Console.WriteLine("Завершение работы системы...");
            Thread.Sleep(1000);
        }
    }
}
