using RDS.Models.RuntimeData.Base;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace RDS.Models.RuntimeData.WorkPanel
{
    public class Model : Notify
    {
        private readonly string[] holeNames = new string[] { Properties.Resources.A, Properties.Resources.B, Properties.Resources.C, Properties.Resources.D };

        private bool isLoaded = false;

        private bool canManualSamping = true;

        private Visibility selectionFrameStates = Visibility.Hidden;

        [XmlAttribute("strName")]
        public string Name { get; set; }

        [XmlAttribute("nPos")]
        public int Position { get; set; }

        [XmlAttribute("strType")]
        public string Strip { get; set; }

        [XmlElement(ElementName = "Slot")]
        public List<Slot> Slots { get; set; }

        [XmlIgnore]
        public Visibility State { get; set; } = Visibility.Hidden;

        [XmlIgnore]
        public bool IsLoaded
        {
            get { return isLoaded; }
            set
            {
                this.State = value ? Visibility.Visible : Visibility.Hidden;
                isLoaded = value;
                this.RaisePropertyChanged(nameof(State));
                this.RaisePropertyChanged(nameof(IsLoaded));
            }
        }

        [XmlIgnore]
        public bool CanManualSampling
        {
            get { return canManualSamping; }
            set
            {
                canManualSamping = value;
                this.RaisePropertyChanged(nameof(CanManualSampling));
            }
        }

        [XmlIgnore]
        public Visibility SelectionFrameStates
        {
            get { return this.selectionFrameStates; }
            set
            {
                this.selectionFrameStates = value;
                this.RaisePropertyChanged(nameof(this.selectionFrameStates));
            }
        }

        public void SetSampleHoleNumber()
        {
            var index = this.Position - 1;
            if (index < 0) index = 0;
            else if (index > 3) index = 3;
            for (int i = 0; i < this.Slots.Count; i++)  this.Slots[i].Name = $"{this.holeNames[index]}{this.Slots[i].Position}";
        }

        public void Clear()
        {
            for (int i = 0; i < this.Slots.Count; i++)
            {
                var patientInfo = this.Slots[i].Tube.Cavities[0].WorkLiquide.Patient;
                this.Slots[i].Tube.Cavities[0].IsEmergency = false;
                patientInfo.Name = string.Empty;
                patientInfo.Age = string.Empty;
                patientInfo.Sex = string.Empty;
                patientInfo.ID = string.Empty;
                patientInfo.Type = string.Empty;
                patientInfo.DateTime = string.Empty;
                patientInfo.Item = string.Empty;
                patientInfo.Barcode = string.Empty;
            }
        }
    }
}
