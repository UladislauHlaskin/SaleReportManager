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
            Thread thread = new Thread(new ThreadStart(StartService));
            thread.Start();
        }

        void StartService()
        {
            _enabled = true;
            Thread.Sleep(1000);
            _changeTracker = ChangeTracker.GetInstance(false);
            _changeTracker.Run(ConfigurationManager.AppSettings.Get("SaleDirectoryPath"));
        }

        protected override void OnStop()
        {
            if (_enabled)
            {
                _changeTracker.Stop();
                _enabled = false;
            }
        }
    }
}
