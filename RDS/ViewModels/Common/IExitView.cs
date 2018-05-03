using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.ViewModels.Common
{
    public interface IExitView
    {
        Action ExitView { get; set; }
    }
}
