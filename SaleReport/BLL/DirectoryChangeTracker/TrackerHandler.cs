using System;
using System.Threading;
using SaleReport.Model;

namespace SaleReport.BLL.DirectoryChangeTracker
{
    class TrackerHandler
    {
        public static void OnFileAddedConsole(object sender, System.IO.FileSystemEventArgs e)
        {
            Thread thread = new Thread(() => AddToDbConsole(e));
            thread.Start();
        }

        public static void OnFileAddedService(object sender, System.IO.FileSystemEventArgs e)
        {
            Thread thread = new Thread(() => AddToDbService(e));
            thread.Start();
        }

        private static void AddToDbConsole(System.IO.FileSystemEventArgs e)
        {
            ConsoleAddingMessage(e);
            Thread.Sleep(100);
            SaleContext context = new SaleContext();
            try
            {
                AddToDb(e, context);
                ConsoleSucceedMessage(e);
            }
            catch (Exception ex)
            {
                ConsoleFailMessage(e);
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                context.Dispose();
            }
        }

        private static void AddToDbService(System.IO.FileSystemEventArgs e)
        {
            Thread.Sleep(100);
            SaleContext context = new SaleContext();
            try
            {
                AddToDb(e, context);
            }
            finally
            {
                context.Dispose();
            }
        }

        private static void AddToDb(System.IO.FileSystemEventArgs e, SaleContext context)
        {
            FileParser.FileParser parser = new FileParser.FileParser(e.FullPath, context);
            File file = parser.Parse();
            context.Files.Add(file);
            context.SaveChanges();
        }

        private static void ConsoleAddingMessage(System.IO.FileSystemEventArgs e)
        {
            Console.WriteLine($"Adding data ({e.Name}) to db...");
        }

        private static void ConsoleSucceedMessage(System.IO.FileSystemEventArgs e)
        {
            Console.WriteLine($"{e.Name} was added to db.");
        }

        private static void ConsoleFailMessage(System.IO.FileSystemEventArgs e)
        {
            Console.WriteLine($"{e.Name} wasn't added to db, something went wrong");
        }
    }
}
