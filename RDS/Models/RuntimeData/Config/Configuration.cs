using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RDS.Models.RuntimeData.Config
{
    /// <summary>
    /// 根配置数据
    /// </summary>
    [DataContract]
    public class Configuration
    {
        [DataMember]
        public List<ReagentGroup> ReagentGroups { get; set; } = new List<ReagentGroup>();
    }
}
