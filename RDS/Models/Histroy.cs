using RDS.Models.RuntimeData.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RDS.Models
{
    public class History : Notify
    {
        [Description("姓名")]
        public string Name { get; set; } = string.Empty;

        [Description("编号")]
        public string Id { get; set; } = string.Empty;

        [Description("性别")]
        public string Sex { get; set; } = string.Empty;

        [Description("年龄")]
        public string Age { get; set; } = string.Empty;

        [Description("类别")]
        public string Type { get; set; } = string.Empty;

        [Description("项目")]
        public string Item { get; set; } = string.Empty;

        [Description("实验日期")]
        public string ExperimentDate { get; set; } = string.Empty;

        [Description("浓度")]
        public string Result { get; set; } = string.Empty;

        [Description("判定")]
        public string Assert { get; set; } = string.Empty;

        [Description("Log值")]
        public string LogValue { get; set; } = string.Empty;

        [Description("DT值")]
        public double DtValue { get; set; }

        [Description("条码")]
        public string Barcode { get; set; } = string.Empty;

        public string[,] Charts { get; set; } 

        [Description("单位")]
        public string Unit { get; set; } = string.Empty;

        public double Concentration { get; set; }

        private static System.Reflection.PropertyInfo[] Properties{get{return typeof(History).GetProperties(); } }

        public static List<string> GetDescriptions()
        {
            List<string> result = new List<string>();
            for (int i = 0; i < Properties.Count(); i++)
            {
                var attributes = Properties[i].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = ((DescriptionAttribute)attributes.FirstOrDefault(o => o.GetType() == typeof(DescriptionAttribute)))?.Description;
               if(description!=null) result.Add(description);
            }
            return result;
        }

        public List<string> GetPropetyValues()
        {
            List<string> result = new List<string>();
            for (int i = 0; i < Properties.Count()-2; i++)
            {
                result.Add(Properties[i].GetValue(this)?.ToString());
            }
            return result;
        }
	}
}
