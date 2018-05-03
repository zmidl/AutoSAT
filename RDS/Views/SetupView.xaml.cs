using RDS.ViewModels;
using RDS.ViewModels.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RDS.Views
{
    /// <summary>
    /// SetupView.xaml 的交互逻辑
    /// </summary>
    public partial class SetupView : UserControl
	{
		public SetupViewModel ViewModel { get { return this.DataContext as SetupViewModel; } }
		public SetupView()
		{
			InitializeComponent();
			this.DataContext = new SetupViewModel();
			this.ViewModel.ViewChanged += ViewModel_ViewChanged;
			ViewModels.Common.General.FindSetupView(this);
		}

		private void ViewModel_ViewChanged(object sender, object e)
		{
			var args = (SetupViewModel.SetupViewChangedArgs)e;
			switch (args.Option)
			{
				case SetupViewModel.ViewChangedOption.ValidateAdministrators:
				{
					var password = args.Value.ToString();
#if DEBUG
                    //password = string.Empty;
#else
                    password = string.Empty;
#endif
                   
                    if (this.PasswordBox1.Password == password) this.ViewModel.ViewIndex = 1;
                    this.ViewModel.ChangeMessage(this.PasswordBox1.Password == password);
                    this.PasswordBox1.Clear();
                    break;
				}
				case SetupViewModel.ViewChangedOption.ClearPassword:
				{
					this.PasswordBox1.Clear();
					break;
				}
			}
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			this.PasswordBox1.Focus();
		}

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            this.ViewModel.GetCapslockState();
        }

        /*code behind*/
        private bool running = false;
		
		private void BtnConvert_Click(object sender, RoutedEventArgs e)
		{
			int num = Convert.ToInt32(TextBoxDsp.Text);
			int t6 = num >> (6 - 1) & 1;
			int t5 = num >> (5 - 1) & 1;
			int t4 = num >> (4 - 1) & 1;
			int t3 = num >> (3 - 1) & 1;
			int t2 = num >> (2 - 1) & 1;
			int t1 = num >> (1 - 1) & 1;

			TextBox6.Text = t6.ToString();
			TextBox5.Text = t5.ToString();
			TextBox4.Text = t4.ToString();
			TextBox3.Text = t3.ToString();
			TextBox2.Text = t2.ToString();
			TextBox1.Text = t1.ToString();
		}

        
        private void Init_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            //this.cM.SendXmlElement(this.T.Text, 0, "C1", "S1");
            var aaa = General.XmlSerialize(General.RuntimeData);
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.CapsLock) this.ViewModel.GetCapslockState();
        }
    }
}
