﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.Models.RuntimeData.WorkPanel
{
    interface IRuntimeProperty
    {
        void RaisePropertyChange();
    }
}
