using RDS.Models.RuntimeData.Base;
using RDS.ViewModels.Common;
using System;

namespace RDS.ViewModels.Result
{
    public class QualitativeSearchViewmodel: Notify
    {
        public Action Close { get; set; }

        public RelayCommand Exit => new RelayCommand( this.Close);

        public QualitativeSearchViewmodel(Action close)
        {
            this.Close = close;
        }
    }
}
