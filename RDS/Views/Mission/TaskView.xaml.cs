using RDS.ViewModels.Common;
using RDS.Views.Mission.Experiment;
using System.Windows;
using System.Windows.Controls;

namespace RDS.Views.Mission
{
    /// <summary>
    /// TaskView.xaml 的交互逻辑
    /// </summary>
    public partial class TaskView : UserControl
    {
        private object PreviousContent;
       

        public TaskView()
        {
            InitializeComponent();
            this.PreviousContent = this.Content;
        }

        private void Button_NewExperiment_Click(object sender, RoutedEventArgs e)
        {
            General.ExitView(this, ((IExitView)new PrecheckView()));
        }


        private void Button_Maintenance_Click(object sender, RoutedEventArgs e)
        {
            General.ExitView(this, ((IExitView)new MaintenanceView()));
        }


        private void FillData2Area0()
        {
            var area = General.RuntimeData.WorkPanel.Areas[0];
            for (int i = 0; i < area.Models.Count; i++)
            {
                var mp = i + 1;
                area.Models[i].Name = $"Strip{mp}";
                area.Models[i].Position = mp;
                for (int j = 0; j < area.Models[i].Slots.Count; j++)
                {
                    var sp = j + 1;
                    var slot = area.Models[i].Slots;
                    slot[j].Name = $"Strip{mp}:RD_SampleRack_TubeSlot{i * 20 + sp}";
                    slot[j].Position = sp;
                    slot[j].Tube.Name = $"RD_SampleTube{i * 20 + sp}";
                    slot[j].Tube.Slot = $"Strip{mp}:RD_SampleRack_TubeSlot{i * 20 + sp}";
                    slot[j].Tube.Cavities[0].Name = $"RD_SampleTube{sp}:RD_SampleTube_Cavity1";
                }
            }
        }

        private void FillData2Area2()
        {
            var area = General.RuntimeData.WorkPanel.Areas[2];
            for (int i = 0; i < area.Models.Count; i++)
            {
                var mp = i + 1;
                area.Models[i].Name = $"RD_CupRack{mp}";
                area.Models[i].Position = mp;
                for (int j = 0; j < area.Models[i].Slots.Count; j++)
                {
                    var sp = j + 1;
                    var slot = area.Models[i].Slots;
                    slot[j].Name = $"RD_CupRack{mp}:RD_CupRack_CupSlot{sp}";
                    slot[j].Position = sp;
                    slot[j].Tube.Name = $"RD_CupRack{sp}";
                    slot[j].Tube.Slot = slot[j].Name;
                    slot[j].Tube.Position = sp;
                    slot[j].Tube.Cavities[0].Name = $"RD_CupRack{mp}:RD_Cup_Cavity{sp}";
                }
            }
        }

        private void FillData2Area3()
        {
            var area = General.RuntimeData.WorkPanel.Areas[3];

            var mp = 1;
            area.Models[0].Name = $"DTRack";
            area.Models[0].Position = mp;
            for (int j = 0; j < area.Models[0].Slots.Count; j++)
            {
                var sp = j + 1;
                var slot = area.Models[0].Slots;
                slot[j].Name = $"DTRack{sp}:Slot";
                slot[j].Position = sp;
                slot[j].Tube.Name = $"DTRack{sp}";
            }
        }

        private void FillData2Area4()
        {
            var area = General.RuntimeData.WorkPanel.Areas[4];
            var mp = 1;
            area.Models[0].Name = $"RD_Heating{mp}";
            area.Models[0].Position = mp;
            for (int j = 0; j < area.Models[0].Slots.Count; j++)
            {
                var sp = j + 1;
                var slot = area.Models[0].Slots;
                slot[j].Name = $"RD_Heating{mp}:RD_Heating_CupSlot{sp}";
                slot[j].Position = sp;
            }
        }

        private void FillData2Area5()
        {
            var area = General.RuntimeData.WorkPanel.Areas[5];
            var mp = 1;
            area.Models[0].Name = $"RD_ShakerRack{mp}";
            area.Models[0].Position = mp;
            for (int j = 0; j < area.Models[0].Slots.Count; j++)
            {
                var sp = j + 1;
                var slot = area.Models[0].Slots;
                slot[j].Name = $"RD_ShakerRack{mp}:RD_Shaker_CupSlot{sp}";
                slot[j].Position = sp;
            }
        }

        private void FillData2Area6()
        {
            var area = General.RuntimeData.WorkPanel.Areas[6];
            var mp = 1;
            area.Models[0].Name = $"RD_Mag{mp}";
            area.Models[0].Position = mp;
            for (int j = 0; j < area.Models[0].Slots.Count; j++)
            {
                var sp = j + 1;
                var slot = area.Models[0].Slots;
                slot[j].Name = $"RD_Mag{mp}:RD_Mag_CupSlot{sp}";
                slot[j].Position = sp;
            }
        }

        private void FillData2Area7()
        {
            var area = General.RuntimeData.WorkPanel.Areas[6];
            var mp = 1;
            area.Models[0].Name = $"RD_Reader{mp}";
            area.Models[0].Position = mp;
            for (int j = 0; j < area.Models[0].Slots.Count; j++)
            {
                var sp = j + 1;
                var slot = area.Models[0].Slots;
                slot[j].Name = $"RD_Reader{mp}:RD_Reader_CupSlot{sp}";
                slot[j].Position = sp;
            }
        }
    }
}
