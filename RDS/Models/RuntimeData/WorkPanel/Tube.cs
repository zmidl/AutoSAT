using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class Tube
    {
        [XmlAttribute("strMemo")]
        public string Memo { get; set; }

        [XmlAttribute("nID")]
        public int ID { get; set; }

        [XmlAttribute("nMaxVolume")]
        public int Max { get; set; }

        [XmlAttribute("nNowVolume")]
        public int Now { get; set; }

        [XmlAttribute("nOver")]
        public int Over { get; set; }

        [XmlAttribute("strMaxVolume")]
        public string MaxVolume { get; set; }

        [XmlAttribute("strMaxNum")]
        public string MaxNum { get; set; }

        [XmlAttribute("nPos")]
        public int Position { get; set; }

        [XmlAttribute("strName")]
        public string Name { get; set; }

        [XmlAttribute("strCurrentSlot")]
        public string Slot { get; set; }

        [XmlAttribute("nType")]
        public int Type { get; set; }

        [XmlAttribute("strTipType")]
        public string TipType { get; set; }

        [XmlAttribute("nTipCount")]
        public int TipCount { get; set; }

        [XmlAttribute("strTipStatus")]
        public string Status { get; set; }

        [XmlElement("Cavity")]
        public List<Cavity> Cavities { get; set; }

        public void LoadCavity(int count,int volume)
        {
            this.Cavities = new List<Cavity>();
            for (int i = 0; i < count; i++) this.Cavities.Add(new Cavity { WorkLiquide=new WorkLiquide { Remain=volume} });
        }
    }
}
