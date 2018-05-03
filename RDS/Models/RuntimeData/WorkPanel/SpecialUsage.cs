using System.Collections.Generic;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class SpecialUsage:DataBase
    {
        [XmlAttribute("nType")]
        public int Type { get; set; }

        [XmlAttribute("strName")]
        public string Name { get; set; }

        [XmlElement("Item")]
        public List<Item> Items { get; set; }
    }
}
