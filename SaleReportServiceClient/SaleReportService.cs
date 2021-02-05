using System;
using System.Configuration;
using System.ServiceProcess;
using SaleReport.DirectoryChangeTracker;

namespace SaleReportServiceClient
{
    partial class SaleReportService : ServiceBase
    {
        ChangeTracker _changeTracker;

        public SaleReportService()
        {
            InitializeComponent();
            this.CanStop = true; 
            this.CanPauseAndContinue = true;
        }

        protected override void OnStart(string[] args)
        {
            _changeTracker = ChangeTracker.GetInstance();
            _changeTracker.Run(ConfigurationManager.AppSettings.Get("SaleDirectoryPath"));
        }

        protected override void OnStop()
        {
            _changeTracker.Stop();
        }
    }
}
