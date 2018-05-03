using RDS.Models.RuntimeData.WorkPanel;
using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RDS.ViewModels.Mission.Experiment
{
    public class TipViewModel : ViewModel
    {
        private bool isReset = true;
        public bool IsReset
        {
            get { return isReset; }
            set
            {
                isReset = true;
                this.RaisePropertyChanged(nameof(IsReset));
            }
        }

        private bool isFull = true;
        public bool IsFull
        {
            get { return isFull; }
            set
            {
                isFull = value;
                this.RaisePropertyChanged(nameof(IsFull));
            }
        }

        private bool isEmpty = true;
        public bool IsEmpty
        {
            get { return isEmpty; }
            set
            {
                isEmpty = value;
                this.RaisePropertyChanged(nameof(IsEmpty));
            }
        }

        public List<Slot> TipRacks { get; set; } = General.RuntimeData.WorkPanel.Areas[3].Models[0].Slots;
        public Slot TipRack { get; set; }
        public Visibility[] State { get; set; } = new Visibility[]
        {
            Visibility.Hidden,Visibility.Hidden,Visibility.Hidden,
            Visibility.Hidden, Visibility.Hidden,Visibility.Hidden,
            Visibility.Hidden,Visibility.Hidden, Visibility.Hidden
        };

        private void Initialize()
        {

        }

        public TipViewModel()
        {

        }

        public RelayCommand SelectTipRack => new RelayCommand((o) =>
        {
            var index = Convert.ToInt32(o);
            this.RaiseStates(index);
            this.TipRack = this.TipRacks[index];
            this.RaisePropertyChanged(nameof(this.TipRack));
        });

        public RelayCommand Exit
        {
            get
            {
                return new RelayCommand(this.ExecuteExitView);
            }
        }

        private void ExecuteExitView()
        {
            //var popupWindow = General.PopupWindow("正在保存信息", new PopupMode[1] { PopupMode.Wait });

            //System.Threading.Tasks.Task.Factory.StartNew((() =>
            //{

            //})).ContinueWith(task =>
            //{
            //    Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            //    {
            //        popupWindow.Close();
            //        this.OnViewChanged(null);
            //    }));
            //});
            this.OnViewChanged(null);
        }

        private void RaiseStates(int index)
        {
            for (int i = 0; i < this.State.Length; i++) this.State[i] = Visibility.Hidden;
            this.State[index] = Visibility.Visible;
            this.RaisePropertyChanged(nameof(this.State));
        }
    }
}
