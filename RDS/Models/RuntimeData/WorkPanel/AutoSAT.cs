using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    [XmlRoot(nameof(AutoSAT))]
    public class AutoSAT:DataBase
    {
        [XmlAttribute("strVer")]
        public string Version { get; set; }

        [XmlElement(nameof(WorkPanel))]
        public WorkPanel WorkPanel { get; set; }

        //[XmlElement(nameof(Container))]
        //public Container Container { get; set; }

        //[XmlElement(nameof(Liquide))]
        //public Liquide Liquide { get; set; }

        [XmlElement(nameof(SpecialUsage))]
        public SpecialUsage SpecialUsage { get; set; }


        
    }
}
