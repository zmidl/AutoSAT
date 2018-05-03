using System;
using System.Windows;

namespace RDS.Apps
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
		//System.Threading.Mutex mutex;
		protected override void OnStartup(StartupEventArgs e)
		{
            base.OnStartup(e);
            this.StartupUri = new Uri(RDS.Properties.Resources.StartupUri, UriKind.Relative);
		}
	}
}
