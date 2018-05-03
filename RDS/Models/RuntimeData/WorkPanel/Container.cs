using System.Collections.Generic;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class Container
    {
        [XmlAttribute("strMemo")]
        public string Memo { get; set; }

        [XmlAttribute("strName")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Tube")]
        public List<Tube> Tubes { get; set; }
    }
}
