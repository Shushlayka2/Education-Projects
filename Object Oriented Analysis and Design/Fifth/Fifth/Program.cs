using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Fifth
{
    class Program
    {
        const string Path = @"..\..\..\..\Exe\";

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

            Task t = ParallelRunningAsync(separate);
            t.Wait();

            WaitTheLastProcessClosing(separate);
            SystemClosing();
        }

        static async Task ParallelRunningAsync(string[] separate)
        {
            int x = 0, y = 245;
            Task[] tasks = new Task[3];
            for (int i = 0; i < 3; i++)
            {
                string fileName = Path + separate[i] + ".exe";
                tasks[i] = RunAsync(fileName, x, y);
                x += 450;
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
                Thread.Sleep(150);
                Program.MoveWindow(p.MainWindowHandle, x, y, 450, 300, true);
            });
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        static void WaitTheLastProcessClosing(string[] separate)
        {
            foreach (Process p in Process.GetProcessesByName(separate[2]))
            {
                p.WaitForExit();
                break;
            }

            for (int i = 0; i < 2; i++)
            {
                foreach (Process p in Process.GetProcessesByName(separate[i]))
                    p.Kill();
            }
        }

        static void SystemClosing()
        {
            Console.WriteLine("Завершение работы системы...");
            Thread.Sleep(1000);
        }
    }
}
