using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RDS.ViewModels.Result
{
    public class SearchViewModel : ViewModel
    {
     
        public event EventHandler<object> EventNotify;

        public void RaiseEvent(object obj) { this.EventNotify?.Invoke(this, obj); }

        public string Begin { get; set; }

        public string End { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public string Barcode { get; set; }

        public string SampleId { get; set; }

        public List<string> AgeItems { get; private set; }

        public List<string> SexItems { get; private set; }

        public List<string> UsedProjects { get; private set; }

        public RelayCommand Stretch => new RelayCommand(() =>
        {
           
        });

        public RelayCommand Exit => new RelayCommand(()=> 
        {
            this.RaiseEvent(null);
        });

        public RelayCommand[] Reset
        {
            get
            {
                return new RelayCommand[]
                {
                    new RelayCommand(()=> this.Name=string.Empty),
                    new RelayCommand(()=> { }),
                    new RelayCommand(()=> this.Barcode=string.Empty),
                    new RelayCommand(()=> this.SampleId=string.Empty),
                    new RelayCommand(()=> this.Begin=string.Empty),
                    new RelayCommand(()=> this.End=string.Empty),
                };
            }
        }

        public SearchViewModel()
        {
            this.InitializeAge();
            this.InitializeSex();
            this.InitializeProject();
        }

        public void InitializeDateTime()
        {
            this.Begin = DateTime.Now.ToString(Properties.Resources.DateFormat);
            this.End = DateTime.Now.ToString(Properties.Resources.DateFormat);
        }

        private void InitializeAge()
        {
            this.AgeItems = new List<string>(150) { General.FindStringResource(Properties.Resources.HistroyView_SexItem_Both) };
            for (int i = 1; i <= 150; i++)
            {
                this.AgeItems.Add(i.ToString());
            }
        }

        private void InitializeProject()
        {
            //this.UsedProjects = General.Configuration.Classes.ToList();
            //this.UsedProjects.Insert(0, General.FindStringResource(Properties.Resources.HistroyView_SexItem_Both));
        }

        private void InitializeSex()
        {
            this.SexItems = new List<string>(3)
            {
                General.FindStringResource(Properties.Resources.HistroyView_SexItem_Both),
                General.FindStringResource(Properties.Resources.HistroyView_SexItem_Man),
                General.FindStringResource(Properties.Resources.HistroyView_SexItem_Woman)
            };
        }
    }
}
