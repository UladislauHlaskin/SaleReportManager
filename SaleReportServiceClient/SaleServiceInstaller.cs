using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace SaleReportServiceClient
{
    [RunInstaller(true)]
    public partial class SaleServiceInstaller : System.Configuration.Install.Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public SaleServiceInstaller()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "SaleReportService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
