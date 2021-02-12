using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using SaleReport.BLL.DirectoryChangeTracker;

namespace SaleReportServiceClient
{
    partial class SaleReportService : ServiceBase
    {
        ChangeTracker _changeTracker;
        bool _enabled;

        public SaleReportService()
        {
            InitializeComponent();
            this.CanStop = true; 
            this.CanPauseAndContinue = true;
            _enabled = false;
        }

        protected override void OnStart(string[] args)
        {
            //_changeTracker = ChangeTracker.GetInstance();
            //_changeTracker.Run(ConfigurationManager.AppSettings.Get("SaleDirectoryPath"));
            Thread thread = new Thread(new ThreadStart(Process));
            thread.Start();
        }

        void Process()
        {
            _enabled = true;
            Thread.Sleep(100);
            //_changeTracker = ChangeTracker.GetInstance();
            //_changeTracker.Run(ConfigurationManager.AppSettings.Get("SaleDirectoryPath"));
            while(_enabled)
            {
                Thread.Sleep(1000);
            }
        }

        protected override void OnStop()
        {
            if (_enabled)
            {
               // _changeTracker.Stop();
                _enabled = false;
            }
        }
    }
}
