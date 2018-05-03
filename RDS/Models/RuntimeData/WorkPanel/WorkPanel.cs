using System.Collections.Generic;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class WorkPanel:DataBase
    {
        [XmlElement(ElementName = "Area")]
        public List<Area> Areas { get; set; }
    }
}
