using System.Collections.Generic;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class Area
    {
        [XmlAttribute("strMemo")]
        public string Memo { get; set; }

        [XmlAttribute("strDataSource")]
        public string DataSource { get; set; }

        [XmlAttribute("strName")]
        public string Name { get; set; }

        [XmlAttribute("nPos")]
        public string Pos { get; set; }

        [XmlElement(ElementName = "Model")]
        public List<Model> Models { get; set; }
    }
}
