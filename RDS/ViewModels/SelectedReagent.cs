using RDS.ViewModels.Common;

namespace RDS.ViewModels
{
	public class SelectedReagent : ViewModel
	{
		private bool isSelected;
		public bool IsSelected
		{
			get { return isSelected; }
			set
			{
				isSelected = value;
				this.RaisePropertyChanged(nameof(IsSelected));
			}
		}

		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				this.RaisePropertyChanged(nameof(Name));
			}
		}

		public SelectedReagent(bool isSelected, string name)
		{
			this.IsSelected = isSelected;
			this.Name = name;
		}
	}
}
