using RDS.Models.RuntimeData.Base;
using RDS.ViewModels.Common;
using System.Windows.Media;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class WorkLiquide : Notify
    {
        private int myCustomVar=123;

        public string ToolTipMessage => this.remainVolume.ToString();

        private int remainVolume = 0;
        private string memo = Properties.Resources.EmptyName;

        private Item item;

        [XmlAttribute("strMemo")]
        public string Memo
        {
            get => string.IsNullOrEmpty(this.memo) ? Properties.Resources.EmptyName : this.memo;
            set
            {
                this.memo = value;
              
               if(string.IsNullOrEmpty(this.memo))
                {
                    this.NameColor = General.WathetColor;
                    this.RemainColor = General.Transparent;
                }
               else
                {
                    this.NameColor=General.BlueColor;
                    this.RemainColor = General.BlueColor;
                }
                this.RaisePropertyChanged(nameof(this.NameColor));
                this.RaisePropertyChanged(nameof(this.Memo));
                this.RaisePropertyChanged(nameof(this.RemainColor));
            }
        }

        [XmlAttribute("nID")]
        public int ID { get; set; }

        [XmlAttribute("nType")]
        public int Type { get; set; }

        [XmlAttribute("strComUsage")]
        public string Usage { get; set; }

        [XmlAttribute("nServingsVolume")]
        public int Volume { get; set; }

        [XmlAttribute("nRemainVolume")]
        public int Remain
        {
            get => this.remainVolume;
            set
            {
                this.remainVolume = value;
                this.ContentColor = value > 0 ? General.BlueColor : this.ContentColor;
                this.RaisePropertyChanged(nameof(this.Remain));
                this.RaisePropertyChanged(nameof(this.ContentColor));
            }
        }

        [XmlElement(ElementName = nameof(Item))]
        public Item Item
        {
            get => this.item;
            set
            {
                this.item = value;
                this.RaisePropertyChanged(nameof(this.Item));
            }
        }


        [XmlElement(ElementName = nameof(Patient))]
        public Patient Patient { get; set; }

        [XmlIgnore]
        public SolidColorBrush ContentColor { get; set; } = General.Transparent;

        [XmlIgnore]
        public SolidColorBrush NameColor { get; set; } = General.WathetColor;

        [XmlIgnore]
        public SolidColorBrush RemainColor { get; set; } = General.Transparent;
    }
}
