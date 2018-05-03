using RDS.Models.RuntimeData.Base;
using RDS.ViewModels.Common;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class Slot : Notify
    {
        private bool isMove = false;
        private bool isLoaded = false;

        [XmlAttribute("strName")]
        public string Name { get; set; }

        [XmlAttribute("nPos")]
        public int Position { get; set; }

        [XmlElement(nameof(Tube))]
        public Tube Tube { get; set; }

        [XmlIgnore]
        public bool IsLoaded
        {
            get { return isLoaded; }
            set
            {
                isLoaded = value;
                this.ContentColor = value ? General.BlueColor2 : General.WathetColor2;
                this.State = value ? Visibility.Visible : Visibility.Hidden;
                this.Icon = value ? General.Out : General.In;
                this.RaisePropertyChanged(nameof(this.State));
                this.RaisePropertyChanged(nameof(this.ContentColor));
                this.RaisePropertyChanged(nameof(IsLoaded));
                this.RaisePropertyChanged(nameof(this.Icon));
            }
        }

        [XmlIgnore]
        public bool IsMove
        {
            get { return this.isMove; }
            set { this.isMove = value;this.RaisePropertyChanged(nameof(this.IsMove)); }
        }

        [XmlIgnore]
        public Visibility State { get; set; }

        [XmlIgnore]
        public PathGeometry Icon { get; set; } = General.Out;

        [XmlIgnore]
        public SolidColorBrush ContentColor { get; set; } = General.WathetColor2;
    }
}
