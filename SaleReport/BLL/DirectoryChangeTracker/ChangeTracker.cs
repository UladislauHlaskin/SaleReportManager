using System;
using System.IO;

namespace SaleReport.BLL.DirectoryChangeTracker
{
    public class ChangeTracker
    {
        private static ChangeTracker _tracker;
        private static object syncRoot = new Object();
        private bool _isRunning = false;
        private bool _isDisposed = false;
        private FileSystemWatcher _watcher;

        public event FileSystemEventHandler FileAdded;

        private ChangeTracker(params FileSystemEventHandler[] onFileAdded)
        {
            FileAdded += TrackerHandler.OnFileAddedConsole;
            foreach (var t in onFileAdded)
            {
                FileAdded += t;
            }
        }

        public static ChangeTracker GetInstance(params FileSystemEventHandler[] onFileAdded)
        {
            if (_tracker == null)
            {
                lock (syncRoot)
                {
                    if (_tracker == null)
                        _tracker = new ChangeTracker(onFileAdded);
                }
            }
            return _tracker;
        }

        public void Run(string directoryPath)
        {
            if (!_isRunning)
            {
                _watcher = new FileSystemWatcher();
                _watcher.Path = directoryPath;
                _watcher.Filter = "*.csv";
                _watcher.Created += FileAdded;
                _watcher.EnableRaisingEvents = true;
                _isRunning = true;
            }
        }

        public void Stop()
        {
            if (!_isDisposed)
            {
                _watcher.Dispose();
                FileAdded = null;
                _tracker = null;
                _isRunning = false;
                _isDisposed = true;
            }
        }
    }
}
