using RDS.ViewModels.Common;
using System;
using System.Windows.Media;
using System.Windows;

namespace RDS.ViewModels
{
    public class PopupWindowViewModel : ViewModel
    {
        public PathGeometry TitleIcon { get; set; }

        public PathGeometry[] ButtonIcon { get; set; }

        public string[] ButtonContent { get; set; }

        public string[] TipMessage { get; set; }

        public Visibility[] ButtonVisibility { get; set; } = new Visibility[] { Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed };

        private bool enableRetryButton = true;
        public bool EnableRetryButton
        {
            get { return enableRetryButton; }
            set
            {
                enableRetryButton = value;
                this.RaisePropertyChanged(nameof(EnableRetryButton));
            }
        }

        public string Message { get; set; } = string.Empty;

        private PopupMode popupType;
        public PopupMode PopupType
        {
            get { return popupType; }
            set
            {
                popupType = value;
                this.RaisePropertyChanged(nameof(PopupType));
                this.PopupTypeIndex = (int)value;
                this.RaisePropertyChanged(nameof(this.PopupTypeIndex));
            }
        }

        public int PopupTypeIndex { get; set; } = 0;

        private Action[] Actions;

        public RelayCommand Command { get; private set; }

        public PopupWindowViewModel()
        {
            this.TipMessage = new string[3];
            this.ButtonIcon = new PathGeometry[3];
            this.ButtonContent = new string[] { string.Empty, string.Empty, string.Empty };
            this.Command = new RelayCommand(this.ExecuteCommand);
        }

        public enum ViewChangedOption
        {
            ExitView = 0,
            EnterAdministratorsView = 1
        }

        public class PopupWindowViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public PopupWindowViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }

        private void ExecuteCommand(object actionIndex)
        {
            int index = Convert.ToInt32(actionIndex);
            if (this.Actions[index] == null) this.Actions[index] = new Action(() => { });
            this.Actions[index]();
            if (this.EnableRetryButton) this.Exit();
        }

        private void Exit()
        {
            this.OnViewChanged(new PopupWindowViewChangedArgs(ViewChangedOption.ExitView, null));
        }

        public void ValidateAdministrators()
        {
            this.OnViewChanged(new PopupWindowViewChangedArgs(ViewChangedOption.EnterAdministratorsView, null));
        }

        private void A(PopupMode[] modes, int index)
        {
            this.ButtonVisibility[index] = Visibility.Visible;
            var name = Enum.GetName(typeof(PopupMode), modes[index]);
            this.ButtonIcon[index] = General.FindPathResource($"P_{name}");
            this.TipMessage[index] = General.FindStringResource($"L_T_{name}");
            this.ButtonContent[index] = this.TipMessage[index];
        }

        public void PopupWindow(string message, PopupMode[] modes, Action[] actions = null)
        {
            if (modes.Length == 1)
            {
                if (modes[0] == PopupMode.Wait) this.PopupTypeIndex = 1;
                else this.A(modes, 0);
            }
            else
            {
                for (int i = 0; i < modes.Length; i++) this.A(modes, i);
            }

            this.Message = message;
            this.Actions = actions;
        }
    }
}
