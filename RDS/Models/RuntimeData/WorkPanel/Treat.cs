using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class Treat
    {
        [XmlAttribute("strMemo")]
        public string Memo { get; set; }

        [XmlAttribute("nType")]
        public int Type { get; set; }

        [XmlElement(nameof(Test))]
        public Test Test { get; set; }
    }
}
