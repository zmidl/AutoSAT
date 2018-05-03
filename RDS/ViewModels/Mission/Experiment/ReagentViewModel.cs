using RDS.Models.RuntimeData.Config;
using RDS.Models.RuntimeData.WorkPanel;
using RDS.ViewModels.Common;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RDS.ViewModels.Mission.Experiment
{
    public class ReagentViewModel : ViewModel
    {
        public AutoSAT RuntimeData => General.RuntimeData;

        public List<ReagentGroup> ReagentGroups => General.Configuration.ReagentGroups;

        private int modelsIndex = 0;
        private int slotsIndex = 0;

        public string SelectedOption { get; set; }

        public object DroppedData { get; set; }

        public List<string> ReagentOptions { get; set; }

        public RelayCommand Exit { get; private set; }

        public RelayCommand DoubleClick => new RelayCommand((o) =>
        {
            //MessageBox.Show("abc");
            var b = (string)o;
            var c = b.Split('_');
            var aaaa = this.DroppedData as ReagentItem;
            RuntimeData.WorkPanel.Areas[1].Models[int.Parse(c[0])].Slots[int.Parse(c[1])].Tube.Cavities[0].WorkLiquide.Memo = string.Empty;
            RuntimeData.WorkPanel.Areas[1].Models[int.Parse(c[0])].Slots[int.Parse(c[1])].Tube.Cavities[0].WorkLiquide.Item.Memo = string.Empty;
        });

        public RelayCommand DragDrop => new RelayCommand((o) =>
        {
            switch (o)
            {
                case null:
                {

                    break;
                }

                case var a when (a is bool):
                {
                    RuntimeData.WorkPanel.Areas[1].Models[0].Slots[0].Tube.Cavities[0].WorkLiquide.Remain = (bool)a ? 100 : 0;
                    break;
                }

                default:
                {
                    var b = (string)o;
                    var c = b.Split('_');
                    if (this.DroppedData != null)
                    {
                        var aaaa = this.DroppedData as ReagentItem;
                        RuntimeData.WorkPanel.Areas[1].Models[int.Parse(c[0])].Slots[int.Parse(c[1])].Tube.Cavities[0].WorkLiquide.Memo = aaaa.Name;
                        RuntimeData.WorkPanel.Areas[1].Models[int.Parse(c[0])].Slots[int.Parse(c[1])].Tube.Cavities[0].WorkLiquide.Item.Memo = aaaa.ParentName;
                    }
                    break;
                }
            }
        });

        public ReagentViewModel()
        {
            this.InitializeExperimentReagents();

            this.SubscribeEvent(true);
            this.Exit = new RelayCommand(() =>
            {
                General.SaveConfiguration();
                this.OnViewChanged(null);
                this.SubscribeEvent(false);
            });
        }

        private void InitializeExperimentReagents()
        {

        }

        private void RefreshUsedItems()
        {

        }

        private void RefreshItemReagentOptions()
        {

        }



        private void SubscribeEvent(bool isSubxdribe)
        {
            for (int i = 0; i < this.ReagentGroups.Count; i++)
            {
                if (isSubxdribe) this.ReagentGroups[i].PropertyChanged += Expanded;
                else this.ReagentGroups[i].PropertyChanged -= Expanded;
            }
        }

        private void Expanded(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var current = sender as ReagentGroup;
            if (e.PropertyName == "IsExpanded" && current.IsExpanded==true)
            {
                for (int i = 0; i < this.ReagentGroups.Count; i++)
                {
                    if(current.Equals(this.ReagentGroups[i])==false) this.ReagentGroups[i].IsExpanded=false;
                }
               
                this.RaisePropertyChanged(nameof(this.ReagentGroups));
            }
        }
    }
}