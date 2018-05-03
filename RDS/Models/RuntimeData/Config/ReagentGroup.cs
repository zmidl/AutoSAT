using RDS.Models.RuntimeData.Base;
using System.Collections.Generic;

namespace RDS.Models.RuntimeData.Config
{
    /// <summary>
    /// 试剂组
    /// </summary>

    public class ReagentGroup:Notify
    {
        private bool isExpanded = false;

        public string Name { get; set; }
      
        public bool IsExpanded
        {
            get => this.isExpanded;
            set
            {
                this.isExpanded = value;
                this.RaisePropertyChanged(nameof(this.IsExpanded));
            }
        }

        public List<ReagentItem> ReagentItems { get; set; } = new List<ReagentItem>();
    }
}
