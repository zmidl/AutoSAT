using RDS.Apps;
using RDS.Models.RuntimeData.Service;
using RDS.ViewModels;
using RDS.ViewModels.Common;
using System.Threading.Tasks;
using System.Windows;

namespace RDS.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainView mainView;

        public ResourceDictionary ResourceDictionary { get; private set; }

        public MainWindowViewModel ViewModel { get { return this.DataContext as MainWindowViewModel; } }

        private CommunicationModule CommunicationModule;

        public MainWindow()
        {
            InitializeComponent();
            General.InitializeMainWindow(this);
            General.LoadLanguage();
          

            if (this.CommunicationModule == null) this.CommunicationModule = new CommunicationModule("C1");
            if (this.CommunicationModule.IsInit == false)
            {
                this.CommunicationModule.Init(this);
                this.CommunicationModule.InitCmds();
                this.CommunicationModule.DataReceived += (s, a) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        General.RuntimeData = General.XmlDeserialize<Models.RuntimeData.WorkPanel.AutoSAT>(a.ToString());
                        this.mainView = new MainView();
                        this.RunAsyncAnimation();
                        SingletonWindow.Process();
                    });
                };
            }
            //Task.Factory.StartNew(() => General.SDK.CalcInit());
            //Task.Factory.StartNew(() => General.SDK.QueryInit());
        }

        private Task<int> OpeningAnimation()
        {
            return Task.Run(() =>
             {
                 for (int i = 0; i < 5; i++)
                 {
                     //System.Threading.Thread.Sleep(1000);
                 }
                 return 1;
             });
        }

        private async void RunAsyncAnimation()
        {
            int a = await OpeningAnimation();
            //MessageBox.Show(string.Empty);
            this.Content = this.mainView;
        }

        // 不阻塞，先往下走。
        private void OpeningAnimation2()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    App.Current.Dispatcher.Invoke(() => { TextBlock_count.Text = i.ToString(); });
                }
                App.Current.Dispatcher.Invoke(() => { this.Content = this.mainView; });
            });
            MessageBox.Show(string.Empty);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //var b = e;
            //var a = System.Windows.Forms.Screen.AllScreens;
            //this.Width = a[0].Bounds.Width;
            //this.Height = a[0].Bounds.Height;
            //this.Left = 0;
            //this.Top = 0;

            this.Left = 0;
            this.Top = 0;
            this.Height = SystemParameters.WorkArea.Height; 
            this.Width = SystemParameters.WorkArea.Width;
        }
    }
}
