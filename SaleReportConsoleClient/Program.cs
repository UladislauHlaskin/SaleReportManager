using System;
using System.IO;
using System.Configuration;
using SaleReport.BLL.DirectoryChangeTracker;
using System.Diagnostics;

namespace SaleReportConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (PriorProcess() == null)
            {
                Track();
            }
        }

        static void Track()
        {
            ChangeTracker changeTracker = ChangeTracker.GetInstance(Watcher_Created);
            changeTracker.Run(ConfigurationManager.AppSettings.Get("SaleDirectoryPath"));

            Console.WriteLine($"Tracking .csv files in {ConfigurationManager.AppSettings.Get("SaleDirectoryPath")}");
            Console.WriteLine("Enter 'e' to exit the application.\n");
            while (Console.Read() != 'e');
        }

        /// <summary>
        /// https://stackoverflow.com/questions/184084/how-to-force-c-sharp-net-app-to-run-only-one-instance-in-windows
        /// </summary>
        public static Process PriorProcess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) &&
                    (p.MainModule.FileName == curr.MainModule.FileName))
                    return p;
            }
            return null;
        }

        private static void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{e.Name} has been added");
        }
    }
}

///
/// TODO
/// 
/// подписи на ивент +
/// переименовать контекст (и строку подключения) +
/// раскидать BLL DAL +
/// CSV парсер ?
/// убрать Console.WriteLine в BLL +
/// exceptions +
/// служба
///