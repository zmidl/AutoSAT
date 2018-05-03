using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace RDS.ViewModels
{
    public class ChartViewModel : ViewModel
    {
        private readonly Brush[] ChartColors = new Brush[6]
        {
            General.BlueColor,
            General.BlueColor,
            General.GreenColor,
            General.GreenColor,
            General.ChartColor1,
            General.ChartColor1
        };

        private readonly string[] ChartNames = new string[6]
        {
            General.FindStringResource(Properties.Resources.Chart_Name1),
            General.FindStringResource(Properties.Resources.Chart_Name2),
            General.FindStringResource(Properties.Resources.Chart_Name3),
            General.FindStringResource(Properties.Resources.Chart_Name4),
            General.FindStringResource(Properties.Resources.Chart_Name5),
            General.FindStringResource(Properties.Resources.Chart_Name6)
        };

        private bool isChannel1Visible;
        public bool IsChannel1Visible
        {
            get { return isChannel1Visible; }
            set
            {
                if (value)
                {
                    isChannel1Visible = value;
                    this.SetLineVisibilityState(0, value);
                    this.SetLineVisibilityState(2, value);
                    this.SetLineVisibilityState(4, value);
                    this.RaisePropertyChanged(nameof(IsChannel1Visible));

                    isChannel2Visible = false;
                    this.SetLineVisibilityState(1, false);
                    this.SetLineVisibilityState(3, false);
                    this.SetLineVisibilityState(5, false);
                    this.RaisePropertyChanged(nameof(IsChannel2Visible));
                }
            }
        }

        private bool isChannel2Visible = true;
        public bool IsChannel2Visible
        {
            get { return isChannel2Visible; }
            set
            {
                if (value)
                {
                    isChannel2Visible = value;
                    this.SetLineVisibilityState(1, value);
                    this.SetLineVisibilityState(3, value);
                    this.SetLineVisibilityState(5, value);
                    this.RaisePropertyChanged(nameof(IsChannel2Visible));

                    isChannel1Visible = false;
                    this.SetLineVisibilityState(0, false);
                    this.SetLineVisibilityState(2, false);
                    this.SetLineVisibilityState(4, false);
                    this.RaisePropertyChanged(nameof(IsChannel1Visible));
                }
            }
        }

        public SeriesCollection SeriesCollection { get; set; }

        public Func<double, string> XFormatter { get; set; }

        public enum ViewChangedOption
        {
            ExitView = 0
        }

        public class ChartViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public ChartViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }

        public RelayCommand ExitView { get; private set; }

        public ChartViewModel(object data)
        {
           if(data!=null)
            {
                this.XFormatter = (value) => string.Format
                (
                    Properties.Resources.StringFormat3,((int)value / 60).ToString(Properties.Resources.DoubleZero),
                    Properties.Resources.Separator3,((int)value % 60).ToString(Properties.Resources.DoubleZero)
                );

                var array = (string[,])data;
                SeriesCollection = new SeriesCollection();
                for (int j = 0; j < 6; j++)
                {
                    var chart1Values = new List<string>(array[j, 0].Split(Properties.Resources.Separator4.ToCharArray()[0]));
                    var chart1Times = new List<string>(array[j, 1].Split(Properties.Resources.Separator4.ToCharArray()[0]));
                    var chart1Series = new ChartValues<ObservablePoint>();
                    for (int i = 0; i < chart1Values.Count; i++)
                    {
                        var time = string.IsNullOrEmpty(chart1Times[i]) ? Properties.Resources.Zero : chart1Times[i];
                        var value = string.IsNullOrEmpty(chart1Values[i]) ? Properties.Resources.Zero : chart1Values[i];
                        chart1Series.Add(new ObservablePoint(double.Parse(time), double.Parse(value)));
                    }

                    this.SeriesCollection.Add(new LineSeries
                    {
                        StrokeDashArray = this.GetLineType(j),// j<2? new DoubleCollection() { 2, 3 }: new DoubleCollection() { 6, 9},
                        Values = chart1Series,
                        Fill = Brushes.Transparent,
                        Stroke = this.ChartColors[j],
                        Title = this.ChartNames[j],
                        PointGeometrySize = 0

                    });
                }
            }
            this.ExitView = new RelayCommand(() => this.OnViewChanged(new ChartViewChangedArgs(ViewChangedOption.ExitView, null)));
        }

        private void SetLineVisibilityState(int index, bool isVisible)
        {
           // ((LineSeries)this.SeriesCollection[index]).Visibility = isVisible ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        private DoubleCollection GetLineType(int index)
        {
            var result = default(DoubleCollection);
            switch (index)
            {
                case 2: case 3: { result = new DoubleCollection() { 1, 4 }; break; }
                case 4: case 5: { result = new DoubleCollection() { 3, 4 }; break; }
            }
            return result;
        }
    }
}
