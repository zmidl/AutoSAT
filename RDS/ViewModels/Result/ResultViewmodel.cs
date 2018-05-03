using RDS.Models.RuntimeData.Base;
using RDS.ViewModels.Common;
using System.Windows;

namespace RDS.ViewModels.Result
{
    public class ResultViewmodel : Notify
    {
        private ExperimentType experimentType = ExperimentType.Qualitative;

        private int experimentTypeIndex = 0;
        public int ExperimentTypeIndex
        {
            get { return experimentTypeIndex; }
            set
            {
                experimentTypeIndex = value;
                this.RaisePropertyChanged(nameof(ExperimentTypeIndex));
            }
        }

        public bool IsQualitative
        {
            get
            {
                return this.experimentType == ExperimentType.Qualitative ? true : false;
            }
            set
            {
                if (value)
                {
                    this.experimentType = ExperimentType.Qualitative;
                    this.RaisePropertyChanged(nameof(this.IsQualitative));
                    this.RaisePropertyChanged(nameof(this.IsQuantitative));
                    this.ExperimentTypeIndex = 0;
                }
            }
        }

        public bool IsQuantitative
        {
            get { return this.experimentType == ExperimentType.Quantitative ? true : false; }
            set
            {
                if (value)
                {
                    this.experimentType = ExperimentType.Quantitative;
                    this.RaisePropertyChanged(nameof(this.IsQualitative));
                    this.RaisePropertyChanged(nameof(this.IsQuantitative));
                    this.ExperimentTypeIndex = 1;
                }
            }
        }

        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                this.RaisePropertyChanged(nameof(IsOpen));
                this.RaisePropertyChanged(nameof(this.PopupState));
            }
        }

        public Visibility PopupState => this.isOpen ? Visibility.Visible : Visibility.Collapsed;

        public QualitativeSearchViewmodel QualitativeSearchViewmodel { get; set; }

        public RelayCommand OpenPopup => new RelayCommand(() =>
        {
            this.IsOpen = true;
        });

        public ResultViewmodel()
        {
            this.QualitativeSearchViewmodel = new QualitativeSearchViewmodel(() => { this.IsOpen = false; });
        }
    }
}
