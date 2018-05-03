using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class Test
    {
        [XmlAttribute("nID")]
        public int ID { get; set; }

        [XmlAttribute("nDispatched")]
        public int Dispatch { get; set; }

        [XmlAttribute("nStep")]
        public int Step { get; set; }

        [XmlAttribute("strStepName")]
        public string StepName { get; set; }

        [XmlElement("nType")]
        public int Type { get; set; }
    }
}
