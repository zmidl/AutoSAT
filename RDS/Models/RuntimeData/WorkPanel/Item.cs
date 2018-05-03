using RDS.Models.RuntimeData.Base;
using RDS.ViewModels.Common;
using System.Collections.Generic;
using System.Windows.Media;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class Item : Notify
    {
        private int status = 0;

        private string memo = string.Empty;

        [XmlAttribute("strMemo")]
        public string Memo
        {
            get => this.memo ?? string.Empty;
            set
            {
                this.memo = value;
                this.NameColor = string.IsNullOrEmpty(value) ? General.Transparent : General.BlueColor;
                this.RaisePropertyChanged(nameof(this.Memo));
                this.RaisePropertyChanged(nameof(this.NameColor));
            }
        }

        [XmlAttribute("strFlowMemo")]
        public string FlowMemo { get; set; }

        [XmlAttribute("strMaterialMemo")]
        public string MaterialMemo { get; set; }

        [XmlAttribute("nID")]
        public int ID { get; set; }

        [XmlAttribute("nType")]
        public int Type { get; set; }

        [XmlAttribute("nFlowType")]
        public int FlowType { get; set; }

        [XmlAttribute("nMaterialType")]
        public int MaterialType { get; set; }

        [XmlAttribute("nStatus")]
        public int Status
        {
            get => this.status;
            set
            {
                this.status = value;
                this.RaisePropertyChanged(nameof(this.Status));
            }
        }

        [XmlElement("Treat")]
        public List<Treat> Treats { get; set; }

        [XmlIgnore]
        public SolidColorBrush NameColor { get; set; } = General.Transparent;

        public void SetStatus(bool status)
        {
            this.status = status ? 1 : 0;
            this.RaisePropertyChanged(nameof(this.Status));
        }
    }
}
