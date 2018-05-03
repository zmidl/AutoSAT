using RDS.Models.RuntimeData.Base;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class DataBase:Notify
    {
        [XmlAttribute("strMemo")]
       public string Memo { get; set; }
    }
}
