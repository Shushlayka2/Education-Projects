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
            Console.WriteLine("Введите три номера проектов для дальнейшего запуска! (от 1 до 5)");
            string answer = Console.ReadLine();
            string[] separate = answer.Split(' ');
            if (separate.Length != 3)
            {
                Console.WriteLine("Неверное количество параметров!");
                SystemClosing();
                return;
            }
            Console.WriteLine("Сколько раз выполнять данную комбинацию программ? (максимум - 3)");
            int amount = int.Parse(Console.ReadLine());

            Task t = ParallelRunningAsync(separate, (amount * 3));
            t.Wait();

            WaitTheLastProcessClosing(separate);
            SystemClosing();
        }

        static async Task ParallelRunningAsync(string[] separate, int amount)
        {
            int x = 0, y = 0;
            Task[] tasks = new Task[amount];
            for (int i = 0; i < amount; i++)
            {
                string fileName = Path + separate[i % 3] + ".exe";
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

        static void WaitTheLastProcessClosing(string[] separate)
        {
            Process lastP = Process.GetProcessById(lastid);
            lastP.WaitForExit();

            for (int i = 0; i < 3; i++)
                foreach (Process p in Process.GetProcessesByName(separate[i]))
                    p.Kill();
        }

        static void SystemClosing()
        {
            Console.WriteLine("Завершение работы системы...");
            Thread.Sleep(1000);
        }
    }
}
