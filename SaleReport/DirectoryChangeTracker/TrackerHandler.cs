using System;
using System.Threading;
using SaleReport.Model;

namespace SaleReport.DirectoryChangeTracker
{
    class TrackerHandler
    {
        public static void OnFileAdded(object sender, System.IO.FileSystemEventArgs e)
        {
            Thread thread = new Thread(() => AddToDb(e));
            thread.Start();
        }

        private static void AddToDb(System.IO.FileSystemEventArgs e)
        {
            Console.WriteLine($"Adding data ({e.Name}) to db...");
            using (SaleContext context = new SaleContext())
            {
                FileParser.FileParser parser = new FileParser.FileParser(e.FullPath, context);
                File file = parser.Parse();
                context.Files.Add(file);
                //context.SaveChangesAsync(); // doesn't work, too much brainlet to find out why
                context.SaveChanges();
            }
            Console.WriteLine($"{e.Name} is in db now.");
        }
    }
}
