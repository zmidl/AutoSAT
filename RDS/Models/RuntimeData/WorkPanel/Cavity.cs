using RDS.Models.RuntimeData.Base;
using RDS.ViewModels.Common;
using System.Windows.Media;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class Cavity : Notify
    {
        private string reagentName = Properties.Resources.EmptyName;
        private bool isTwinkle = false;
        private bool isLoaded = false;
        private bool isEmergency;

        [XmlAttribute("strName")]
        public string Name { get; set; }

        [XmlAttribute("nStatus")]
        public int Status { get; set; }

        [XmlAttribute("nPos")]
        public int Position { get; set; }

        [XmlAttribute("nCount")]
        public int Count { get; set; }

        [XmlAttribute("nRemainVolume")]
        public int RemainVolume { get; set; }

        [XmlElement(nameof(WorkLiquide))]
        public WorkLiquide WorkLiquide { get; set; }

        [XmlIgnore]
        public string ItemReagentName
        {
            get { return string.IsNullOrEmpty(this.reagentName) ? Properties.Resources.EmptyName : this.reagentName; }
            set
            {
                reagentName = value;
                this.RaisePropertyChanged(nameof(ItemReagentName));
            }
        }

        [XmlIgnore]
        public string HoleNumber { get; set; }

        [XmlIgnore]
        public bool IsEmergency
        {
            get { return isEmergency; }
            set
            {
                isEmergency = value;
                this.RaisePropertyChanged(nameof(IsEmergency));
            }
        }

        [XmlIgnore]
        public bool IsLoaded
        {
            get { return isLoaded; }
            set
            {
                isLoaded = value;
                this.ContentColor = value ? General.BlueColor : new SolidColorBrush(Colors.White);
                this.RaisePropertyChanged(nameof(this.ContentColor));
                this.RaisePropertyChanged(nameof(IsLoaded));
            }
        }

        [XmlIgnore]
        public bool IsTwinkle
        {
            get { return this.isTwinkle; }
            set { this.isTwinkle = value; this.RaisePropertyChanged(nameof(this.IsTwinkle)); }
        }

        [XmlIgnore]
        public SolidColorBrush ContentColor { get; set; } = new SolidColorBrush(Colors.White);

       
    }
}
