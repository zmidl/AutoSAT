using RDS.Models.RuntimeData.Base;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class Patient : Notify
    {
        private string item = string.Empty;

        private string dateTime = string.Empty;

        [XmlAttribute("strMemo")]
        public string Memo { get; set; }

        [XmlAttribute("strName")]
        public string Name { get; set; }

        [XmlAttribute("strSampleType")]
        public string Type { get; set; }

        [XmlAttribute("strSampleID")]
        public string ID { get; set; }

        [XmlAttribute("strDateTime")]
        public string DateTime
        {
            get { return dateTime; }
            set
            {
                //if (value.Length > 8) this.dateTime = Convert.ToDateTime(value.ToString()).ToString("yyyy-MM-dd");
                //else dateTime = value;
                dateTime = value;
                this.RaisePropertyChanged(nameof(DateTime));
            }
        }

        [XmlAttribute("strSex")]
        public string Sex { get; set; }

        [XmlAttribute("strAge")]
        public string Age { get; set; }

        [XmlAttribute("strBirthday")]
        public string Birthday { get; set; }

        [XmlAttribute("strItem")]
        public string Item
        {
            get { return this.item; }
            set { this.item = value;this.RaisePropertyChanged(nameof(this.Item)); }
        }

        [XmlAttribute("strBarcode")]
        public string Barcode { get; set; }
    }
}
