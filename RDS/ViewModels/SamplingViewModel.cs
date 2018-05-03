using RDS.Models;
using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.ViewModels
{
	public class SamplingViewModel:ViewModel
	{
		
		public enum ViewChangedOption
		{
			ExitView = 0
		}

		public class SamplingViewChangedArgs : EventArgs
		{
			public ViewChangedOption Option { get; set; }
			public object Value { get; set; }

			public SamplingViewChangedArgs(ViewChangedOption option, object value)
			{
				this.Option = option;
				this.Value = value;
			}
		}

		public RelayCommand ExitView { get; set; }

		public SamplingViewModel(SampleInformation[] sampleInformations)
		{
			
			this.ExitView = new RelayCommand(this.ExecuteExitView);
		}

		private void ExecuteExitView()
		{
			this.OnViewChanged(new SamplingViewChangedArgs(ViewChangedOption.ExitView, null));
		}
	}
}
